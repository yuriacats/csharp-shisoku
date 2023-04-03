set -eu
function main(){
    run_test_case "足し算" 1+1 2 
    run_test_case "掛け算" 1*1 1 
    run_test_case "（）付きの計算式" "(1+1)*3" 6
    run_test_case "（）の後に数字もしくは（）が来ている" "(1+1)(3)" error 
    run_test_case "空白は異常終了する" "(1+1) (3)" error
    run_test_case "(が終了していない" "(1+1" error
    run_test_case "文字列を入れると失敗する" "aaaa" error
    run_test_case "演算記号のみを入れると失敗する" "---" error
    run_test_case "演算記号のみを入れると失敗する" "1-2" -1 

}

function run_test_case(){
    local case_name=($1)
    local input=($2)
    local expected=($3)

    local output=$(dotnet run --no-build --project shisoku -- --exp ${input} 2> /dev/null)
    local error_code=$?
    if [ "${output}" = "${expected}" ];then
        echo "${case_name}:Pass"
        return 0
    elif [ ${expected} = "error" ] && [ ${error_code} -eq '0' ];then
        echo "${case_name}:Pass"
        return 0
    else
        echo "${case_name}:Fail" 
        echo "${input} == ${expected}ではありません"
        return 1
    fi
}
main
