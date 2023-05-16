#!/bin/bash
function main() {
    local sum_exit_code=0
    run_test_exp_case "足し算" "1+1;" 2
    sum_exit_code=$((sum_exit_code + $?))
    run_test_exp_case "掛け算" "1*1;" 1
    sum_exit_code=$((sum_exit_code + $?))
    run_test_exp_case "（）付きの計算式" "(1+1)*3;" 6
    sum_exit_code=$((sum_exit_code + $?))
    run_test_exp_case "割算が優先" "1+6/3;" 3
    sum_exit_code=$((sum_exit_code + $?))
    run_test_exp_case "（）の後に（）が来ている" "(1+1)(3)" error
    sum_exit_code=$((sum_exit_code + $?))
    # なぜ上が正しくないかというとパースした後に全てのTokenが使い切れる構文木が生成されないから。
    # Tokenが余っていたら弾く
    # TODO 本体コードを修正する
    run_test_exp_case "空白は全て無視するよって正しい計算式が空白で区切られても問題ない" "1+1 +2;" 4
    sum_exit_code=$((sum_exit_code + $?))
    run_test_exp_case "(が終了していない" "(1+1" error
    sum_exit_code=$((sum_exit_code + $?))
    run_test_exp_case "文字列を入れると失敗する" "aaaa" error
    sum_exit_code=$((sum_exit_code + $?))
    run_test_exp_case "演算記号のみを入れると失敗する" "---" error
    sum_exit_code=$((sum_exit_code + $?))
    run_test_exp_case "正解が負の数になっても失敗しない" "1-2;" -1
    sum_exit_code=$((sum_exit_code + $?))
    run_test_exp_case "変数の定義をしても問題ない" "const a=1;" ""
    sum_exit_code=$((sum_exit_code + $?))
    run_test_exp_case "変数の計算をしても問題ない" "const a=1;a+1;" error
    sum_exit_code=$((sum_exit_code + $?))
    run_test_exp_case "複数の計算を出せる" "1-2;1;" error
    sum_exit_code=$((sum_exit_code + $?))
    run_test_exp_case "Booleanが扱える" "1==1;" True
    sum_exit_code=$((sum_exit_code + $?))
    run_test_exp_case "Booleanが扱える(False)" "2==1;" False 
    sum_exit_code=$((sum_exit_code + $?))
    run_test_exp_case "Booleanが扱える(bool同士)" "true==true;" True 
    sum_exit_code=$((sum_exit_code + $?))
    run_test_exp_case "Booleanが混ざった複合演算ができる" "true== (1+1 == 2);" True 
    sum_exit_code=$((sum_exit_code + $?))
    run_test_file_case "複数分の入ったファイルを読み込んで実行できる" "shisoku.Integration/test.shisoku" "2" 
    sum_exit_code=$((sum_exit_code + $?))

    if [ ${sum_exit_code} == "0" ]; then
        return 0
    else
        return 1
    fi
}

function run_test_exp_case() {
    local case_name="$1"
    local input="$2"
    local expected="$3"

    local output
    output=$(dotnet run --no-build --project shisoku -- --exp "${input}" 2>/dev/null)
    local error_code=$?
    if [ "${output}" = "${expected}" ]; then
        echo "${case_name}:Pass"
        return 0
    elif [ "${expected}" = "error" ] && [ "${error_code}" -ne '0' ]; then
        echo "${case_name}:Pass"
        return 0
    else
        echo "${case_name}:Fail output is ${output}. Shoud be ${expected}"
        return 1
    fi
}
function run_test_file_case() {
    local case_name="$1"
    local input="$2"
    local expected="$3"

    local output
    output=$(dotnet run --no-build --project shisoku -- --file "${input}" 2>/dev/null)
    local error_code=$?
    if [ "${output}" = "${expected}" ]; then
        echo "${case_name}:Pass"
        return 0
    elif [ "${expected}" = "error" ] && [ "${error_code}" -ne '0' ]; then
        echo "${case_name}:Pass"
        return 0
    else
        echo "${case_name}:Fail output is ${output}. Shoud be ${expected}"
        return 1
    fi
}
main
