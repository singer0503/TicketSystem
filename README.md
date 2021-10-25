# **TicketSystem**
## **相關文件撰寫中...**
感謝考官的出題

目前的結果

![動畫2.gif](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/3018b9e1-943c-4b3a-b742-61cb784e80a6/動畫2.gif)

## 目錄結構說明

`SQLscript` 該資料夾存放建立 SQL 所需的 Script 

`TicketSystem` 後端程式碼

`vue-ticket-system` 前端程式碼

### **Quick start 快速開始**

> 請檢查您的 Node 以及 npm 以及 dotnet Core 的版本, Node版本 > 10.5 , npm > 6.8 , dotnet core = 3.1
> 

複製一份至本地端 git clone 

```bash
git clone https://github.com/singer0503/TicketSystem
```

設定與 SQL Server 資料庫連接字串，設定檔案 appsettings.json

```bash
cd TicketSystem
vim appsettings.json
```

Sample: `Data Source=(computer name)\\SQLEXPRESS;Initial Catalog=TicketSystem;User ID=username;Password=password;`

後端執行套件還原 using dotnet cli restore package

```bash
cd TicketSystem
dotnet restore TicketSystem.csproj
```

執行後端 backend start

```bash
dotnet run
```