using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVManager : MonoBehaviour
{
    //外部數據 (public)
    public string fileName = "GlobalValue";
    public List<FileValue> fileValue = new List<FileValue>();
    public string filePath;


    //內部資料 (private) {測試用時，會打開成public查看數據}
    private bool open_Create = false;  //用來重新建立檔案用 (第一次開啟遊戲 跟 Determine_OpenValue偵測不到值時觸發)


    public void Start()
    {
        //定義文件路徑
        filePath = Application.streamingAssetsPath + '/' + fileName + ".csv";

        //檢驗文件是否存在，如果不存在則創建文件。
        if(!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }

        //如果檔案不存在(尚未建立)，則建立進行寫入。 <- 只有在第一次開啟遊戲時會作用
        if(!System.IO.File.Exists(filePath))
        {
            //創建寫入變數，定義儲存路徑
            StreamWriter streamWriter = new StreamWriter(filePath);

            //寫入內容數據
            for(int i = 0 ; i < fileValue.Count ; i++)
            {
                streamWriter.WriteLine($"{fileValue[i].name},{fileValue[i].open_string}");
            }
        
            //推送到文件夾
            streamWriter.Flush();

            //文件關閉
            streamWriter.Close();
        }
    }


    //偵測該變數的開關狀態
    public bool Determine_OpenValue(string input)
    {
        //建立string output
        bool output = false;

        //創建讀取變數，定義儲存路徑
        StreamReader streamReader = new StreamReader(filePath);

        //先建立變數，讀取第一行 <- 偵錯用
        string fileString = streamReader.ReadLine();
            //如果第一行為空，則open_Create開啟，重建檔案。
            if(fileString == null)
            {
                open_Create = true;
            }

        //讀取變數狀態
        while(fileString != null)
        {
            FileValue fileValue = new FileValue();
            fileValue = String_To_FileValue(fileString);

            if(fileValue.name == input)
            {
                if(fileValue.open_string == "TRUE")
                    output = true;
                else if(fileValue.open_string == "FALSE")
                    output = false;
                else
                    open_Create = true;     //偵錯用，如果觸發則重建檔案。
                
                break;
            }

            fileString = streamReader.ReadLine();
        }

        //文件關閉
        streamReader.Close();

        //如果偵錯觸發 (open_Create = true) ，則重建檔案
        if(open_Create)
        {
            //創建寫入變數，定義儲存路徑
            StreamWriter streamWriter = new StreamWriter(filePath);

            //寫入內容數據
            for(int i = 0 ; i < fileValue.Count ; i++)
            {
                streamWriter.WriteLine($"{fileValue[i].name},{fileValue[i].open_string}");
            }
        
            //推送到文件夾
            streamWriter.Flush();

            //文件關閉
            streamWriter.Close();

            open_Create = false;
        }

        //回傳狀態
        return output;
    }

    //寫入該變數的狀態更新
    public void Change_OpenValue(string input_name ,bool input_bool)
    {
        //建立一個暫存的list，用以儲存寫入資料。
        List<string> list_String = new List<string>();
        List<FileValue> list_fileValue = new List<FileValue>();


        //創建讀取變數，定義儲存路徑
        StreamReader streamReader = new StreamReader(filePath);

        //讀取變數狀態
        for(string fileString = streamReader.ReadLine() ; fileString != null ; fileString = streamReader.ReadLine())
        {
            // 先把讀取的(單行)字串，拆成資料型態儲存。
            FileValue fileValue = new FileValue();
            fileValue = String_To_FileValue(fileString);

            //如果是要更改(input)的那行資料，則更改(open_string)狀態
            if(fileValue.name == input_name)
            {
                if(input_bool == true)
                    fileValue.open_string = "TRUE";
                else
                    fileValue.open_string = "FALSE";
            }

            //然後把讀到的字串儲存在list_fileValue裡。
            list_fileValue.Add(fileValue);
        }
        
        //文件關閉
        streamReader.Close();


        //創建寫入變數，定義儲存路徑
        StreamWriter streamWriter = new StreamWriter(filePath);

        //寫入變數狀態  (把上面讀到的資料，全部重新打上)
        foreach(FileValue temp_fileValue in list_fileValue)
        {
            streamWriter.WriteLine($"{temp_fileValue.name},{temp_fileValue.open_string}");
        }
        
        //推送到文件夾 (覆蓋文件)
        streamWriter.Flush();

        //文件關閉
        streamWriter.Close();
    }

    //String -> FileValue
    private FileValue String_To_FileValue(string input)
    {
        FileValue output = new FileValue();

        string[] temp = input.Split(',');
        output.name = temp[0];
        output.open_string = temp[1];

        return output;
    }
}


//自定義儲存資料，用以控制(開關)使用變數。
[System.Serializable]
public class FileValue
{
    public string name;
    public string open_string;
}