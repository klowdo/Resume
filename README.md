# Resume

My CV as code. `profile.yaml` holds the content, a .NET tool renders it to PDF with [QuestPDF](https://www.questpdf.com/).

Latest build: [resume.pdf](https://github.com/klowdo/Resume/releases/latest/download/resume.pdf)

## Usage

```sh
just build       # out/resume.pdf
just anonymous   # out/resume-anonymous.pdf, no contact details
just preview     # rebuild on change and open in zathura
just companion   # QuestPDF Companion live preview
```

Requires .NET 10 and `just`. `nix develop` (or direnv) provides both.

## Editing

Edit `profile.yaml`. The schema reference at the top of the file gives completion and validation in editors with the YAML language server. Regenerate it from the contracts:

```sh
dotnet run --project Flixen.CurriculumVitae.Contracts.SchemaGenerator \
  -- ./Flixen.CurriculumVitae.Contracts.SchemaGenerator/schema.json
```

Colors, fonts and profile picture live in `Flixen.CurriculumVitae.Builder/theme.yaml`.

## Layout

| Project | Purpose |
| --- | --- |
| `Flixen.CurriculumVitae.Models` | Profile and theme types |
| `Flixen.CurriculumVitae.Layouts` | QuestPDF components |
| `Flixen.CurriculumVitae.Builder` | CLI entry point |
| `Flixen.CurriculumVitae.Contracts.SchemaGenerator` | Emits `schema.json` |

## CLI

```
dotnet run -- [--config-file <path>] [--theme-file <path>] [--anonymous] <write --output <path> | live>
```

Releases are cut by release-please; tagging publishes the PDF as a release asset.
