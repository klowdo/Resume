# Changelog

## [1.0.0](https://github.com/klowdo/Resume/compare/v0.0.3...v1.0.0) (2026-08-10)


### ⚠ BREAKING CHANGES

* profile.yaml renames workItems to employers, replaces contact.address with contact.location and changes skills to categories.

### Features

* add nix flake with dotnet-sdk 10 dev shell ([c054495](https://github.com/klowdo/Resume/commit/c054495d33a849f5a68ca0d2f33b542c6762a48d))
* **design:** decrease size for side bar ([dede758](https://github.com/klowdo/Resume/commit/dede7580beb25e663a8f9305f031a547237ef2a7))
* model experience as employer/engagement hierarchy ([c86d0a5](https://github.com/klowdo/Resume/commit/c86d0a5fe69d873edf87197f64f476b4a5c441c8))
* **profile:** add evolve and bash ([28a72ce](https://github.com/klowdo/Resume/commit/28a72ce956a34dec852b37fa49063d03e33690a9))
* source resume data from shared profile.yaml; net10 ([2099e92](https://github.com/klowdo/Resume/commit/2099e924ddc7176e85a25fbb2ebbb7b0d312c237))
* upgrade QuestPDF 2026 + System.CommandLine 2.0; drop SkiaSharp ([5e50e7f](https://github.com/klowdo/Resume/commit/5e50e7f4aeb74f2ad3bc667087057e5ef6d8afb6))


### Bug Fixes

* **layout:** keep employer header with its first engagement ([332d77e](https://github.com/klowdo/Resume/commit/332d77e14975876f4bcd602a075a087b4a645564))
* **profile:** correct NixOS casing and network switches wording ([6c40838](https://github.com/klowdo/Resume/commit/6c40838b2567fe936935a023a376772ee518e8db))
* use inline lambda for Canvas delegate compatibility ([c06b5ac](https://github.com/klowdo/Resume/commit/c06b5ac969534b94e9461a76ea1dd094eb0aadff))
