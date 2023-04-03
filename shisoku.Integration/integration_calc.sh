set -eu
function main(){
    run_test_case 1+1 2 
    run_test_case 1*1 1 
    run_test_case "(1+1)*3" 6
    run_test_case "aaaa" error
}

function run_test_case(){
    local input=($1)
    local expected=($2)

    local output=$(dotnet run --project shisoku -- --exp ${input})
    local error_code=$?
    if [ ${output} = ${expected} ];then
        echo "${input} == ${expected}です"
        return 0
    elif [ ${expected} = "error" ] && [ ${error_code} -eq '0' ];then
        echo "想定通りに異常終了しました"
        return 0
    else
        echo "${input} == ${expected}ではありません"
        return 1
    fi
}
main
