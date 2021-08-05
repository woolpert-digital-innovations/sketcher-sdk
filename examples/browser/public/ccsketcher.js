
if (window['CAMACloud'] === undefined) {
  CAMACloud = {};
}

CAMACloud.Sketcher = function (options) {
  let returnData;
  let editorWindow;
  let removeMessageHandler;

  const url = (options && options.url) || 'https://app.sketcher.camacloud.com';

  this.open = function (options) {
    this.close();
    const o = options || {};
    returnData = o.data || {};
    const params = [
      'height=' + (screen.height > 700 ? screen.height : 700),
      'width=' + (screen.width > 1200 ? screen.width : 1200),
      'resizable=yes'
    ].join(',');
    editorWindow = window.open(url, 'sketcheditor', params);
    removeMessageHandler = createMessageHandler(o);
  };

  this.close = function () {
    if (removeMessageHandler) {
      removeMessageHandler();
      removeMessageHandler = null;
    }
    if (editorWindow) {
      editorWindow.close();
      editorWindow = null;
    }
  };

  Object.defineProperties(this, {
    data: {
      get: function () {
        return returnData;
      }
    }
  });

  Object.defineProperties(this, {
    isEditorClosed: {
      get: function () {
        return !editorWindow || editorWindow.closed;
      }
    }
  });

  const stringifyMessage = function (message) {
    let messageString = null;
    try {
      messageString = JSON.stringify(message);
    } catch (error) {
      console.log('Unable to stringify message.', error);
    }
    return messageString;
  };

  const parseMessage = function (messageString) {
    let message = {};
    try {
      message = JSON.parse(messageString);
    } catch (error) {
      console.log('Unable to parse message.', error);
    }
    return message;
  };

  const createMessageHandler = function (options) {
    const o = options || {};
    let removeHandler = null;
    const messageHandler = (function (event) {
      const message = parseMessage(event.data);
      if (!message) {
        return;
      }
      if (message.type === 'ready') {
        load(o.data, o.config);
      } else if (message.type === 'log') {
        if (o.onLog) {
          o.onLog(message.data);
        }
      } else if (message.type === 'save') {
        save(message.data, o.onSave);
      } else if (message.type === 'closed') {
        editorWindow = null;
        removeHandler();
        if (o.onClosed) {
          o.onClosed();
        }
      }
    }).bind(this);
    removeHandler = function () {
      window.removeEventListener('message', messageHandler, false);
    };
    window.addEventListener('message', messageHandler, false);
    return removeHandler;
  };

  const load = function (data, config) {
    if (!editorWindow) {
      return;
    }
    editorWindow.postMessage(stringifyMessage({
      type: 'load',
      data: { data: data, config: config }
    }), url);
    returnData = data;
  };

  const save = function (data, onSave) {
    const onFailure = function (error) {
      if (!editorWindow) {
        return;
      }
      const errorMessage = (error && error.message && error.message.toString()) ||
        (error && error.toString()) || 'Unrecognized';
      editorWindow.postMessage(stringifyMessage({ type: 'save', data: errorMessage }), url);
    };
    const onSuccess = function () {
      if (editorWindow) {
        editorWindow.postMessage(stringifyMessage({ type: 'save' }), url);
      }
      returnData = data;
    };
    if (onSave) {
      onSave(data, onSuccess.bind(this), onFailure.bind(this));
    } else {
      onSuccess();
    }
  };
};
