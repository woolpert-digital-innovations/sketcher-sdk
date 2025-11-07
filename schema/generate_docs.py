from typing import Type
from pathlib import Path
import shutil
import tempfile
from json_schema_for_humans.generate import generate_from_file_object
from json_schema_for_humans.generation_configuration import GenerationConfiguration
import re
import requests

skip_versions: list[str] = ["0.10"]


def generate_docs(schema_file, schema_name):
    outdir = Path("../docs/sds/schemas")
    shutil.rmtree(outdir, ignore_errors=True)
    outdir.mkdir(parents=True, exist_ok=True)
    doc = outdir.joinpath(f"{schema_name}.md")

    config = GenerationConfiguration(
        template_name="md",
        show_toc=True,
        link_to_reused_ref=True,
        collapse_long_descriptions=False,
        footer_show_time=False,
    )
    with open(doc, "w+") as doc_file:
        generate_from_file_object(schema_file, doc_file, config=config)
    print(f"Created {doc}...")


def create_schema_url_by_version() -> dict[str, str]:
    with open("../docs/sds/release-notes.md", "r") as file:
        content = file.read()
        matches = re.findall(
            r"\[(\d+\.\d+)\]\((https:\/\/schemas\.opencamadata\.org\/[^/]+\/data\.schema\.json)\)",
            content,
        )
        return dict(matches)


schema_url_by_version = create_schema_url_by_version()

for version, url in schema_url_by_version.items():
    if version in skip_versions:
        continue

    r = requests.get(url)
    with tempfile.NamedTemporaryFile(mode="w+t", delete=True) as temp_file:
        temp_file.write(r.text)
        temp_file.seek(0)
        generate_docs(temp_file, version)
