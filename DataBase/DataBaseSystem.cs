using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Mastear.DataBase
{
    [Serializable]
    public class DataBaseSystem
    {
        public System system;

        public void Load()
        {
            try
            {
                Stream FileStream = File.Open("dataBase.erika", FileMode.Open, FileAccess.Read);
                BinaryFormatter bf = new BinaryFormatter();
                system = (System)bf.Deserialize(FileStream);
                FileStream.Close();
            }
            //파일로드 에러발생시 비어있는 객체 새로 생성
            catch
            {
                system = new System();
            }
        }

        public void Save()
        {
            Stream FileStream = File.Open("dataBase.erika", FileMode.Create, FileAccess.ReadWrite);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(FileStream, system);
            FileStream.Close();
        }
    }
}
