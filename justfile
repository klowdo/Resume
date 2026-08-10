proj := "Flixen.CurriculumVitae.Builder"
root := justfile_directory()

default: build

build out="out/resume.pdf":
    cd {{proj}} && dotnet run -- write --output {{root / out}}

anonymous out="out/resume-anonymous.pdf":
    cd {{proj}} && dotnet run -- --anonymous write --output {{root / out}}

all: build anonymous

preview out="out/resume.pdf": (build out)
    # ponytail: sleep instead of detecting dotnet watch's first write; bump it if the viewer opens on a half-written pdf
    (sleep 5 && zathura {{root / out}}) &
    cd {{proj}} && dotnet watch run -- write --output {{root / out}}

companion:
    cd {{proj}} && dotnet run -- live

clean:
    rm -rf out
