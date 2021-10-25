# **TicketSystem**
## **相關文件撰寫中...**
感謝考官的出題

目前的結果

![動畫2.gif](https://grizzled-coat-756.notion.site/image/https%3A%2F%2Fs3-us-west-2.amazonaws.com%2Fsecure.notion-static.com%2F3018b9e1-943c-4b3a-b742-61cb784e80a6%2F%E5%8B%95%E7%95%AB2.gif?table=block&id=98572ddb-e2b4-4ebf-92cc-8fe216de7847&spaceId=9923dfc3-318e-4dec-aaef-cfd17e7f129b&userId=&cache=v2)

## 目錄結構說明

![TicketSysTem-程式架構圖.png](https://grizzled-coat-756.notion.site/image/https%3A%2F%2Fs3-us-west-2.amazonaws.com%2Fsecure.notion-static.com%2F064612aa-5d67-48bd-bf1b-ef33d56eab1e%2FTicketSysTem-%E7%A8%8B%E5%BC%8F%E6%9E%B6%E6%A7%8B%E5%9C%96.png?table=block&id=8eb00f39-ecc4-44e3-9ab3-153dcfcc4b24&spaceId=9923dfc3-318e-4dec-aaef-cfd17e7f129b&width=1720&userId=&cache=v2)

`SQLscript` 該資料夾存放建立 SQL 所需的 Script 

`TicketSystem` 後端程式碼 (Net Core 3.1)

`vue-ticket-system` 前端程式碼 (Vue.js 2)

### **Quick start 快速開始**

> 請檢查您的 Node 以及 npm 以及 dotnet Core 的版本, Node版本 > 10.5 , npm > 6.8 , dotnet core = 3.1
> 

複製一份至本地端 git clone 

```bash
git clone https://github.com/singer0503/TicketSystem
```

設定後端與 SQL Server 資料庫連接字串，設定檔案 appsettings.json

設定後端與 SQL Server 資料庫連接字串，設定檔案 `appsettings.json`

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

進入

```bash
cd ..
cd vue-ticket-system
npm install
```

```bash
//...在最下方
externals: {
    // global app config object
    config: JSON.stringify({
        //apiUrl: 'http://localhost:4000'
        apiUrl: 'https://localhost:44337'
    })
}
```

執行前端專案

```bash
npm start
```