/*
 * 파일 쓰기, 읽기 담당
 * 파일 저장은 '/사용자폴더/AppData/Local/yofp/TodoCalendar' 에 저장할 예정
 */
using System.IO;
using System.Text.Json;

namespace TODO_List.Common.Util
{
    public static class FileHelper
    {
        // Path.Combie - 운영체제에 맞게 경로 설정할때 슬래쉬, 역슬래쉬 자동 처리해주는 함수
        // Environment.GetFolderPath - 폴더 경로 가져오는 함수
        // Environment.SpecialFolder.LocalApplicationData - 윈도우 환경설정에서 "Local AppData"로 지정된 절대 경로를 자동으로 찾아줌
        private static readonly string FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "yofp", "TodoCalendar");

        /// <summary>
        /// 데이터를 Json 형식으로 변환하여 저장
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">파일 이름</param>
        /// <param name="data">저장할 데이터</param>
        public static void SaveJson<T>(string fileName, T data)
        {
            // 경로가 없으면 yofp 폴더와 TodoCalendar 폴더를 계층적으로 생성
            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);

            string filePath = Path.Combine(FolderPath, fileName);

            var options = new JsonSerializerOptions // Json 옵션 설정
            { 
                WriteIndented = true, // 사람이 보기쉽게 줄바꿈, 들여쓰기 할것인지
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull }; // 데이터가 null일 경우 Json 파일에 항목 기록 자체를 하지않음
            string jsonString = JsonSerializer.Serialize(data, options); // data를 Json 형식으로 변환(options 적용해서)
            File.WriteAllText(filePath, jsonString); // filePath 경로에 파일이 존재하는지 확인하고 jsonString 내용으로 덮어쓰기, 없으면 생성
        }

        /// <summary>
        /// Json 형식의 데이터 불러오기
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">파일 이름</param>
        /// <returns></returns>
        public static T? LoadJson<T>(string fileName) where T : class
        {
            string filePath = Path.Combine(FolderPath, fileName);
            if (!File.Exists(filePath))
                return null;

            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(jsonString); // Json 형식의 data를 컴퓨터가 읽기 쉽게 변환해서 return
        }
    }
}
