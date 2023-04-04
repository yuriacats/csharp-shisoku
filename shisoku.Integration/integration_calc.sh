function main(){
    local exit_all=0
    run_test_case "足し算" 1+1 2 
    exit_all=$((exit_all+$?))
    run_test_case "掛け算" 1*1 1 
    exit_all=$((exit_all+$?))
    run_test_case "（）付きの計算式" "(1+1)*3" 6
    exit_all=$((exit_all+$?))
    run_test_case "割算が優先" "1+6/3" 3
    exit_all=$((exit_all+$?))
    #run_test_case "（）の後に（）が来ている" "(1+1)(3)" error
    #exit_all=$((exit_all+$?))
    # なぜ上が正しくないかというとパースした後に全てのTokenが使い切れる構文木が生成されないから。 
    # Tokenが余っていたら弾く
    # TODO 本体コードを修正する
    run_test_case "空白は全て無視するよって正しい計算式が空白で区切られても問題ない" "1+1 +2" 4 
    exit_all=$((exit_all+$?))
    run_test_case "(が終了していない" "(1+1" error
    exit_all=$((exit_all+$?))
    run_test_case "文字列を入れると失敗する" "aaaa" error
    exit_all=$((exit_all+$?))
    run_test_case "演算記号のみを入れると失敗する" "---" error
    exit_all=$((exit_all+$?))
    run_test_case "正解が負の数になっても失敗しない" "1-2" -1 
    exit_all=$((exit_all+$?))
    
    if [ ${exit_all} == "0" ];then
        return 0
    else
        return 1
    fi 
}

function run_test_case(){
    local case_name="$1"
    local input="$2"
    local expected="$3"

    local output
    output=$(dotnet run --no-build --project shisoku -- --exp "${input}" 2> /dev/null)
    local error_code=$?
    if [ "${output}" = "${expected}" ];then
        echo "${case_name}:Pass"
        return 0
    elif [ ${expected} = "error" ] && [ ${error_code} -ne '0' ];then
        echo "${case_name}:Pass"
        return 0
    else
        echo "${case_name}:Fail output is ${output}. Be ${expected}" 
        return 1
    fi
}
main
