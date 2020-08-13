using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastear.DataBase
{
    [Serializable]
    public class System
    {
        //등록한 액추에이터 리스트
        public List<Actuator> actuatorList = new List<Actuator>();
        //학습파라미터 정보
        public Learn learn = new Learn();
        //이 시스템이 사용할 경로
        public String path;
    }
}
