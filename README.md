# csharp-shisoku
このリポジトリーは、オリジナルプログラミング言語を作っているリポジトリーです（？）

## How To Use

```terminal
$ dotnet run --project shisoku
```

## Road　Map

```mermaid
mindmap
  root((Road Map))
    テストの拡充
      ASTのテストの追加
      インテグレーションテストの追加
    言語処理系としての実装
      変数の実装 
        予約語の制定
      関数の実装
        キーワード引数のみを扱う関数にする
        関数型言語チックな言語にする
      型機能の実装
        関数の接頭辞を必ず型名にする
          細かい型の場合型を後ろに書く構文をつける
          細かい方の場合それを包含する大きな型を描く
      ファイルから処理を行う仕組みの作成
      ランゲージサーバーの実装
      パッケージ管理システムの実装
      開発環境作成
        コードからテストケースの推論を行うコマンド
        コードのカバレッジを測定する標準ライブラリーの作成
        テストを強要する仕組みの作成
    UI改善
      Repl以外のインターフェースの作成
      ASTを表示しないモードの作成
    処理系のRust移行
      言語名の制定
      言語コンセプトとサイト、ドメインの取得
    関数拡張
      SUM関数の作成
      累乗の作成
      割ったあまりを出す関数の作成
    環境整備
      CD、配布のためのActionsの制定
      コードカバレッジを測るツールの導入
```
