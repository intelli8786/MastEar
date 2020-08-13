using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastear.DataBase
{
    /*
     * 학습에 필요한 파라미터 저장
     */
    [Serializable]
    public class Learn
    {
        //마지막 학습시간
        public DateTime LastLearnTime;

        //전문가모드 활성화유무
        public Boolean IsMasterMode;

        //사용할 음성의 길이설정
        public String SoundLength;

        //히든레이어 갯수 설정
        public String HiddenLayer1;
        public String HiddenLayer2;
        public String HiddenLayer3;

        //학습횟수
        int epoch;
        //학습결과 그래프
        public float[] LearningGraph; //길이는 epoch
        //학습률
        public float LearningLate;
        
        //사용한 피쳐
        public Boolean MFCC;
        public Boolean CROMA;
        public Boolean MelSpectrogram;
        public Boolean Contrast;
        public Boolean Tonnetz;
    }
}
