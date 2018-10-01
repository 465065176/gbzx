using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fiddler;

namespace 贵州省干部在线学习助手
{
    public class chinahrt
    {
        public static void FiddlerApplication_BeforeRequest(Session oSession) {
            if (
                (oSession.url.IndexOf("/videoPlay/play?") > 0) ||
                (oSession.url.IndexOf("/js/player/adksplayer.js?") > 0)
                )
            {
                //词句代码必须，不然无法修改返回数据
                oSession.bBufferResponse = true;
            }
        }

        public static void FiddlerApplication_BeforeResponse(Session oSession) {
            if (oSession.url.IndexOf("/js/player/adksplayer.js?") > 0)
            {
                oSession.utilDecodeResponse();
                //bool r = oSession.utilReplaceInResponse("//CKobject.getObjectById(playerId).addListener('loadComplete','loadCompleteHandler');", "CKobject.getObjectById(playerId).addListener('loadComplete','loadCompleteHandler');");
            }
            else if (oSession.url.IndexOf("/videoPlay/play?") > 0)
            {
                oSession.utilDecodeResponse();
                bool r = oSession.utilReplaceInResponse("attrset.ifPauseBlur=true;", "attrset.ifPauseBlur=false;");
                r = oSession.utilReplaceInResponse("attrset.autoPlay=0;", "attrset.autoPlay = 1;");
                


            }
        }
    }
}
