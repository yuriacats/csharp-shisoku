set -eu
function main(){
    test $(dotnet run --project shisoku -- --exp 1+1) -eq 2
    test $(dotnet run --project shisoku -- --exp 1*1) -eq 1 
    test $(dotnet run --project shisoku -- --exp "(1+1)*3" ) -eq 6
}

main
