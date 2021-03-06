﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class ClassBankInfo3a1
    {
        public class BankUtil
        {
            //public static void main(String[] args)
            //{
            //    String cardNumber = "6210676802084484923";//卡号
            //    String name = getNameOfBank(cardNumber);
            //    System.out.println(name);
            //}


            //传入卡号 得到银行名称
            public static String getNameOfBank(String idCard)
            {
                int index = -1;

                if (idCard == null || idCard.Length < 15 || idCard.Length > 19)//部分卡15位
                {
                    return "";
                }

                //5位Bin号
                String cardbin_5 = idCard.Substring(0, 5);
                for (int i = 0; i < bankBin.Length; i++)
                {
                    if (cardbin_5.Equals(bankBin[i]))
                    {
                        index = i;
                    }
                }
                if (index != -1)
                {
                    return bankName[index];
                }

                //6位Bin号
                String cardbin_6 = idCard.Substring(0, 6);
                for (int i = 0; i < bankBin.Length; i++)
                {
                    if (cardbin_6.Equals(bankBin[i]))
                    {
                        index = i;
                    }
                }
                if (index != -1)
                {
                    return bankName[index];
                }

                //8位Bin号
                String cardbin_8 = idCard.Substring(0, 8);
                for (int i = 0; i < bankBin.Length; i++)
                {
                    if (cardbin_8.Equals(bankBin[i]))
                    {
                        index = i;
                    }
                }
                if (index != -1)
                {
                    return bankName[index];
                }

                return "";
            }

            //BIN号
            private static String[] bankBin = {


                "621096","621098","622150","622151","622181","622188","622199","955100","621095","620062","621285","621798","621799","621797","620529","621622","621599","621674","623218","623219","623698",
"62215049","62215050","62215051","62218850","62218851","62218849",
"622812","622810","622811","628310","625919",
"620200","620302","620402","620403","620404","620406","620407","620409","620410","620411","620412","620502","620503","620405","620408","620512","620602","620604","620607","620611","620612","620704","620706","620707","620708","620709","620710","620609","620712","620713","620714","620802","620711","620904","620905","621001","620902","621103","621105","621106","621107","621102","621203","621204","621205","621206","621207","621208","621209","621210","621302","621303","621202","621305","621306","621307","621309","621311","621313","621211","621315","621304","621402","621404","621405","621406","621407","621408","621409","621410","621502","621317","621511","621602","621603","621604","621605","621608","621609","621610","621611","621612","621613","621614","621615","621616","621617","621607","621606","621804","621807","621813","621814","621817","621901","621904","621905","621906","621907","621908","621909","621910","621911","621912","621913","621915","622002","621903","622004","622005","622006","622007","622008","622010","622011","622012","621914","622015","622016","622003","622018","622019","622020","622102","622103","622104","622105","622013","622111","622114","622017","622110","622303","622304","622305","622306","622307","622308","622309","622314","622315","622317","622302","622402","622403","622404","622313","622504","622505","622509","622513","622517","622502","622604","622605","622606","622510","622703","622715","622806","622902","622903","622706","623002","623006","623008","623011","623012","622904","623015","623100","623202","623301","623400","623500","623602","623803","623901","623014","624100","624200","624301","624402","623700","624000",
"622200","622202","622203","622208","621225","620058","621281","900000","621558","621559","621722","621723","620086","621226","621618","620516","621227","621288","621721","900010","623062","621670","621720","621379","621240","621724","621762","621414","621375","622926","622927","622928","622929","622930","622931","621733","621732","621372","621369","621763",
"402791","427028","427038","548259","621376","621423","621428","621434","621761","621749","621300","621378","622944","622949","621371","621730","621734","621433","621370","621764","621464","621765","621750","621377","621367","621374","621731","621781",
"370246","370248","370249","370247","370267","374738","374739",
"427010","427018","427019","427020","427029","427030","427039","438125","438126","451804","451810","451811","458071","489734","489735","489736","510529","427062","524091","427064","530970","530990","558360","524047","525498","622230","622231","622232","622233","622234","622235","622237","622239","622240","622245","622238","451804","451810","451811","458071","628288","628286","622206","526836","513685","543098","458441","622246","544210","548943","356879","356880","356881","356882","528856","625330","625331","625332","622236","524374","550213","625929","625927","625939","625987","625930","625114","622159","625021","625022","625932","622889","625900","625915","625916","622171","625931","625113","625928","625914","625986","625925","625921","625926","625942","622158","625917","625922","625934","625933","625920","625924","625017","625018","625019",
"45806","53098","45806","53098",
"622210","622211","622212","622213","622214","622220","622223","622225","622229","622215","622224",
"620054","620142","620184","620030","620050","620143","620149","620124","620183","620094","620186","620148","620185",
"620114","620187","620046",
"622841","622824","622826","622848","620059","621282","622828","622823","621336","621619","622821","622822","622825","622827","622845","622849","623018","623206","621671","622840","622843","622844","622846","622847","620501",
"95595","95596","95597","95598","95599",
"403361","404117","404118","404119","404120","404121","463758","519412","519413","520082","520083","552599","558730","514027","622836","622837","628268","625996","625998","625997","622838","625336","625826","625827","544243","548478","628269",
"622820","622830","623052",
"621660","621661","621662","621663","621665","621667","621668","621669","621666","456351","601382","621256","621212","621283","620061","621725","621330","621331","621332","621333","621297","621568","621569","621672","623208","621620","621756","621757","621758","621759","621785","621786","621787","621788","621789","621790","622273","622274","622771","622772","622770","621741","621041",
"621293","621294","621342","621343","621364","621394","621648","621248","621215","621249","621231","621638","621334","621395","623040","622348",
"625908","625910","625909","356833","356835","409665","409666","409668","409669","409670","409671","409672","512315","512316","512411","512412","514957","409667","438088","552742","553131","514958","622760","628388","518377","622788","628313","628312","622750","622751","625145","622479","622480","622789","625140","622346","622347",
"518378","518379","518474","518475","518476","524865","525745","525746","547766","558868","622752","622753","622755","524864","622757","622758","622759","622761","622762","622763","622756","622754","622764","622765","558869","625905","625906","625907","625333",
"53591","49102","377677",
"620514","620025","620026","620210","620211","620019","620035","620202","620203","620048","620515","920000",
"620040","620531","620513","921000","620038",
"621284","436742","589970","620060","621081","621467","621598","621621","621700","622280","622700","623211","623668",
"421349","434061","434062","524094","526410","552245","621080","621082","621466","621488","621499","622966","622988","622382","621487","621083","621084","620107",
"436742193","622280193",
"553242",
"625362","625363","628316","628317","356896","356899","356895","436718","436738","436745","436748","489592","531693","532450","532458","544887","552801","557080","558895","559051","622166","622168","622708","625964","625965","625966","628266","628366","622381","622675","622676","622677",
"5453242","5491031","5544033",
"622725","622728","436728","453242","491031","544033","622707","625955","625956",
"53242","53243",
"622261","622260","622262","621002","621069","621436","621335",
"620013",
"405512","601428","405512","601428","622258","622259","405512","601428",
"49104","53783",
"434910","458123","458124","520169","522964","552853","622250","622251","521899","622253","622656","628216","622252","955590","955591","955592","955593","628218","625028","625029",
"622254","622255","622256","622257","622284",
"620021","620521",
"402658","410062","468203","512425","524011","622580","622588","622598","622609","95555","621286","621483","621485","621486","621299",
"690755",
"690755",
"356885","356886","356887","356888","356890","439188","439227","479228","479229","521302","356889","545620","545621","545947","545948","552534","552587","622575","622576","622577","622578","622579","545619","622581","622582","545623","628290","439225","518710","518718","628362","439226","628262","625802","625803",
"370285","370286","370287","370289",
"620520",
"622615","622616","622618","622622","622617","622619","415599","421393","421865","427570","427571","472067","472068","622620",
"545392","545393","545431","545447","356859","356857","407405","421869","421870","421871","512466","356856","528948","552288","622600","622601","622602","517636","622621","628258","556610","622603","464580","464581","523952","545217","553161","356858","622623","625912","625913","625911",
"377155","377152","377153","377158","621691",
"303",
"90030",
"620535",
"620085","622660","622662","622663","622664","622665","622666","622667","622669","622670","622671","622672","622668","622661","622674","622673","620518","621489","621492",
"356837","356838","486497","622657","622685","622659","622687","625978","625980","625981","625979","356839","356840","406252","406254","425862","481699","524090","543159","622161","622570","622650","622655","622658","625975","625977","628201","628202","625339","625976",
"433670","433680","442729","442730","620082","622690","622691","622692","622696","622698","622998","622999","433671","968807","968808","968809","621771","621767","621768","621770","621772","621773","622453","622456",
"622459",
"376968","376969","376966",
"400360","403391","403392","404158","404159","404171","404172","404173","404174","404157","433667","433668","433669","514906","403393","520108","433666","558916","622678","622679","622680","622688","622689","628206","556617","628209","518212","628208","356390","356391","356392","622916","622918","622919",
"622630","622631","622632","622633","999999","621222","623020","623021","623022","623023",
"523959","528709","539867","539868","622637","622638","628318","528708","622636","625967","625968","625969",
"621626","623058",
"602907","622986","622989","622298","627069","627068","627066","627067","412963","415752","415753","622535","622536","622538","622539","998800","412962","622983",
"531659","622157","528020","622155","622156","526855","356869","356868","625360","625361","628296","435744","435745","483536","622525","622526","998801","998802",
"620010",
"438589",
"90592",
"966666","622909","438588","622908",
"461982","486493","486494","486861","523036","451289","527414","528057","622901","622902","622922","628212","451290","524070","625084","625085","625086","625087","548738","549633","552398","625082","625083","625960","625961","625962","625963",
"621050","622172","622985","622987","620522","622267","622278","622279","622468","622892","940021",
"438600",
"356827","356828","356830","402673","402674","486466","519498","520131","524031","548838","622148","622149","622268","356829","622300","628230","622269","625099","625953",
"622516","622517","622518","622521","622522","622523","984301","984303","621352","621793","621795","621796","621351","621390","621792","621791",
"84301","84336","84373","84385","84390","87000","87010","87030","87040","84380","84361","87050","84342",
"356851","356852","404738","404739","456418","498451","515672","356850","517650","525998","622177","622277","628222","622500","628221","622176","622276","622228","625957","625958","625993","625831",
"622520","622519",
"620530",
"622516","622517","622518","622521","622522","622523","984301","984303","621352","621793","621795","621796","621351","621390","621791",
"622568","6858001","6858009","621462",
"9111",
"406365","406366","428911","436768","436769","436770","487013","491032","491033","491034","491035","491036","491037","491038","436771","518364","520152","520382","541709","541710","548844","552794","493427","622555","622556","622557","622558","622559","622560","528931","558894","625072","625071","628260","628259","625805","625806","625807","625808","625809","625810",
"685800","6858000",
"621268","622684","622884","621453",
"603445","622467","940016","621463",
"622449","940051",
"622450","628204",
"621977",
"622868","622899","628255",
"622877","622879","621775","623203",
"603601","622137","622327","622340","622366",
"628251","622651","625828",
"621076","622173","622131","621579","622876",
"504923","622422","622447","940076",
"628210","622283","625902",
"621777","622305","621259",
"622303","628242","622595","622596",
"621279","622281","622316","940022",
"621418",
"625903","622778","628207","512431","520194","622282","622318",
"623111","421317","422161","602969","422160","621030","621420","621468",
"522001","622163","622853","628203","622851","622852",
"620088","621068","622138","621066","621560",
"625526","625186","628336",
"622946",
"622406","621442",
"622407","621443",
"622360","622361","625034","625096","625098",
"622948","621740","622942","622994",
"622482","622483","622484",
"621062","621063",
"625076","625077","625074","625075","622371","625091",
"622933","622938","623031","622943","621411",
"22372","622471","622472","622265","622266","625972","625973",
"622365",
"621469","621625",
"622128","622129","623035",
"909810","940035","621522","622439",
"622328","940062","623038",
"625288","625888",
"622333","940050",
"621439","623010",
"622888",
"622302",
"622477","622509","622510","622362","621018","621518",
"622297","621277",
"622375","622489",
"622293","622295","622296","622373","622451","622294","625940",
"622871","622958","622963","622957","622861","622932","622862","621298",
"622798","625010","622775","622785",
"621016","621015",
"622487","622490","622491","622492",
"622487","622490","622491","622492","621744","621745","621746","621747",
"623078",
"622384","940034",
"940015","622331",
"6091201",
"622426","628205",
"621091",
"621019","622309","621019",
"6223091100","6223092900","6223093310","6223093320","6223093330","6223093370","6223093380","6223096510","6223097910",
"621213","621289","621290","621291","621292","621042","621743",
"623041","622351",
"625046","625044","625058","622349","622350",
"620208","620209","625093","625095",
"622393","940023",
"6886592",
"623019","621600",
"622388",
"621267","623063",
"620043",
"622865","623131",
"940012",
"622178","622179","628358",
"990027",
"622325","623105","623029",
"566666",
"622455","940039",
"623108","623081",
"622466","628285",
"603708",
"622993","623069","623070","623172","623173",
"622383","622385","628299",
"622498","622499","623000","940046",
"622921","628321",
"621751","622143","940001","621754",
"622476","628278",
"622486",
"603602","623026","623086",
"628291",
"622152","622154","622996","622997","940027","622153","622135","621482","621532",
"622442",
"940053",
"622442","623099",
"622421",
"940056",
"96828",
"621529","622429","621417","623089","623200",
"628214","625529","622428",
"9896",
"622134","940018","623016",
"621577","622425",
"940049",
"622425",
"622139","940040","628263",
"621242","621538","621496",
"621252","622146","940061","628239",
"621419","623170",
"62249802","94004602",
"621237","623003",
"622310","940068",
"622817","628287","625959",
"62536601",
"622427",
"940069",
"623039",
"622321","628273",
"625001",
"694301",
"940071","622368","621446",
"625901","622898","622900","628281","628282","622806","628283",
"620519",
"683970","940074",
"622370",
"621437",
"628319",
"622336","621760",
"622165",
"622315","625950","628295",
"621037","621097","621588","622977",
"62321601",
"622860",
"622644","628333",
"622478","940013","621495",
"625500",
"622611","622722","628211","625989",
"622717",
"628275","622565","62228",
"622147","621633",
"628252",
"623001",
"628227",
"621456",
"621562",
"628219",
"621037","621097","621588","622977",
"62321601",
"622475","622860",
"625588",
"622270","628368","625090","622644","628333",
"623088",
"622829","628301","622808","628308",
"622127","622184","621701","621251","621589","623036",
"628232","622802","622290",
"622531","622329",
"622829","628301",
"621578","623066","622452","622324",
"622815","622816","628226",
"622906","628386","625519","625506",
"621592",
"628392",
"621748",
"628271",
"621366","621388",
"628328",
"621239","623068",
"621653004",
"622169","621519","621539","623090",
"621238","620528",
"628382","625158",
"621004",
"628217",
"621416",
"628217",
"622937",
"628397",
"628229",
"622469","628307",
"622292","622291","621412",
"622880","622881",
"62829",
"623102",
"628234",
"628306",
"622391","940072",
"628391",
"622967","940073",
"628233",
"628257",
"621269","622275",
"940006",
"628305",
"622133","621735",
"888",
"628213",
"622990","940003",
"628261",
"622311","940057",
"628311",
"622363","940048",
"628270",
"622398","940054",
"940055",
"622397",
"603367","622878","623061",
"622397","622286","628236","625800",
"603506",
"622399","940043",
"622420","940041",
"622338",
"940032",
"622394","940025",
"621245",
"621328",
"621651",
"621077",
"622409","621441",
"622410","621440",
"622950","622951",
"625026","625024","622376","622378","622377","625092",
"622359","940066",
"622886",
"940008","622379",
"628379",
"620011","620027","620031","620039","620103","620106","620120","620123","620125","620220","620278","620812","621006","621011","621012","621020","621023","621025","621027","621031","620132","621039","621078","621220","621003",
"625003","625011","625012","625020","625023","625025","625027","625031","621032","625039","625078","625079","625103","625106","625006","625112","625120","625123","625125","625127","625131","625032","625139","625178","625179","625220","625320","625111","625132","625244",
"622355","623042",
"621043","621742",
"622352","622353","625048","625053","625060",
"620206","620207",
"622547","622548","622546",
"625198","625196","625147",
"620072",
"620204","620205",
"621064","622941","622974",
"622493",
"621274","621324"

            };


            //"发卡行.卡种名称", 
            private static String[] bankName = {


                "中国邮政储蓄银行","中国邮政储蓄银行","中国邮政储蓄银行","中国邮政储蓄银行","中国邮政储蓄银行","中国邮政储蓄银行","中国邮政储蓄银行","中国邮政储蓄银行","中国邮政储蓄银行","中国邮政储蓄银行","中国邮政储蓄银行","中国邮政储蓄银行","中国邮政储蓄银行","中国邮政储蓄银行","中国邮政储蓄银行","中国邮政储蓄银行","中国邮政储蓄银行","中国邮政储蓄银行","中国邮政储蓄银行","中国邮政储蓄银行","中国邮政储蓄银行",
"中国邮政储蓄银行","中国邮政储蓄银行","中国邮政储蓄银行","中国邮政储蓄银行","中国邮政储蓄银行","中国邮政储蓄银行",
"中国邮政储蓄银行","中国邮政储蓄银行","中国邮政储蓄银行","中国邮政储蓄银行","中国邮政储蓄银行",
"中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行",
"中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行",
"中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行",
"中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行",
"中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行",
"中国工商银行","中国工商银行","中国工商银行","中国工商银行",
"中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行",
"中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行","中国工商银行",
"中国工商银行","中国工商银行","中国工商银行",
"中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行",
"中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行",
"中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行","中国农业银行",
"中国农业银行","中国农业银行","中国农业银行",
"中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行",
"中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行",
"中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行",
"中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行",
"中国银行","中国银行","中国银行",
"中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行","中国银行",
"中国银行","中国银行","中国银行","中国银行","中国银行",
"中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行",
"中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行",
"中国建设银行","中国建设银行",
"中国建设银行",
"中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行",
"中国建设银行","中国建设银行","中国建设银行",
"中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行","中国建设银行",
"中国建设银行","中国建设银行",
"中国交通银行","中国交通银行","中国交通银行","中国交通银行","中国交通银行","中国交通银行","中国交通银行",
"中国交通银行",
"中国交通银行","中国交通银行","中国交通银行","中国交通银行","中国交通银行","中国交通银行","中国交通银行","中国交通银行",
"中国交通银行","中国交通银行",
"中国交通银行","中国交通银行","中国交通银行","中国交通银行","中国交通银行","中国交通银行","中国交通银行","中国交通银行","中国交通银行","中国交通银行","中国交通银行","中国交通银行","中国交通银行","中国交通银行","中国交通银行","中国交通银行","中国交通银行","中国交通银行","中国交通银行","中国交通银行",
"中国交通银行","中国交通银行","中国交通银行","中国交通银行","中国交通银行",
"中国交通银行","中国交通银行",
"招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行",
"招商银行",
"招商银行",
"招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行","招商银行",
"招商银行","招商银行","招商银行","招商银行",
"招商银行",
"中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行",
"中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行",
"中国民生银行","中国民生银行","中国民生银行","中国民生银行","中国民生银行",
"中国光大银行",
"中国光大银行",
"中国光大银行",
"中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行",
"中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行","中国光大银行",
"中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行",
"中信银行",
"中信银行","中信银行","中信银行",
"中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行","中信银行",
"华夏银行","华夏银行","华夏银行","华夏银行","华夏银行","华夏银行","华夏银行","华夏银行","华夏银行","华夏银行",
"华夏银行","华夏银行","华夏银行","华夏银行","华夏银行","华夏银行","华夏银行","华夏银行","华夏银行","华夏银行","华夏银行","华夏银行",
"平安银行（原深圳发展银行）","平安银行（原深圳发展银行）",
"平安银行（原深圳发展银行）","平安银行（原深圳发展银行）","平安银行（原深圳发展银行）","平安银行（原深圳发展银行）","平安银行（原深圳发展银行）","平安银行（原深圳发展银行）","平安银行（原深圳发展银行）","平安银行（原深圳发展银行）","平安银行（原深圳发展银行）","平安银行（原深圳发展银行）","平安银行（原深圳发展银行）","平安银行（原深圳发展银行）","平安银行（原深圳发展银行）","平安银行（原深圳发展银行）","平安银行（原深圳发展银行）","平安银行（原深圳发展银行）","平安银行（原深圳发展银行）","平安银行（原深圳发展银行）",
"平安银行（原深圳发展银行）","平安银行（原深圳发展银行）","平安银行（原深圳发展银行）","平安银行（原深圳发展银行）","平安银行（原深圳发展银行）","平安银行（原深圳发展银行）","平安银行（原深圳发展银行）","平安银行（原深圳发展银行）","平安银行（原深圳发展银行）","平安银行（原深圳发展银行）","平安银行（原深圳发展银行）","平安银行（原深圳发展银行）","平安银行（原深圳发展银行）","平安银行（原深圳发展银行）","平安银行（原深圳发展银行）","平安银行（原深圳发展银行）","平安银行（原深圳发展银行）","平安银行（原深圳发展银行）",
"平安银行（原深圳发展银行）",
"兴业银行",
"兴业银行",
"兴业银行","兴业银行","兴业银行","兴业银行",
"兴业银行","兴业银行","兴业银行","兴业银行","兴业银行","兴业银行","兴业银行","兴业银行","兴业银行","兴业银行","兴业银行","兴业银行","兴业银行","兴业银行","兴业银行","兴业银行","兴业银行","兴业银行","兴业银行","兴业银行","兴业银行","兴业银行","兴业银行","兴业银行","兴业银行","兴业银行","兴业银行",
"上海银行","上海银行","上海银行","上海银行","上海银行","上海银行","上海银行","上海银行","上海银行","上海银行","上海银行",
"上海银行",
"上海银行","上海银行","上海银行","上海银行","上海银行","上海银行","上海银行","上海银行","上海银行","上海银行","上海银行","上海银行","上海银行","上海银行","上海银行","上海银行","上海银行","上海银行","上海银行",
"浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行",
"浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行",
"浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行","浦东发展银行",
"浦东发展银行","浦东发展银行",
"浦东发展银行",
"广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行",
"广发银行","广发银行","广发银行","广发银行",
"广发银行",
"广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行","广发银行",
"广发银行","广发银行",
"渤海银行","渤海银行","渤海银行","渤海银行",
"广州银行","广州银行","广州银行","广州银行",
"金华银行","金华银行",
"金华银行","金华银行",
"温州银行",
"温州银行","温州银行","温州银行",
"徽商银行","徽商银行","徽商银行","徽商银行",
"徽商银行","徽商银行","徽商银行","徽商银行","徽商银行",
"徽商银行","徽商银行","徽商银行",
"江苏银行","江苏银行","江苏银行","江苏银行","江苏银行",
"江苏银行","江苏银行","江苏银行","江苏银行",
"江苏银行","江苏银行","江苏银行",
"南京银行","南京银行","南京银行",
"南京银行","南京银行","南京银行","南京银行",
"宁波银行","宁波银行","宁波银行","宁波银行",
"宁波银行",
"宁波银行","宁波银行","宁波银行","宁波银行","宁波银行","宁波银行","宁波银行",
"北京银行","北京银行","北京银行","北京银行","北京银行","北京银行","北京银行","北京银行",
"北京银行","北京银行","北京银行","北京银行","北京银行","北京银行",
"北京农村商业银行","北京农村商业银行","北京农村商业银行","北京农村商业银行","北京农村商业银行",
"北京农村商业银行","北京农村商业银行","北京农村商业银行",
"汇丰银行",
"汇丰银行","汇丰银行",
"汇丰银行","汇丰银行",
"汇丰银行","汇丰银行","汇丰银行","汇丰银行","汇丰银行",
"渣打银行","渣打银行","渣打银行","渣打银行",
"渣打银行","渣打银行","渣打银行",
"花旗银行","花旗银行",
"花旗银行","花旗银行","花旗银行","花旗银行","花旗银行","花旗银行",
"东亚银行","东亚银行","东亚银行","东亚银行","东亚银行",
"东亚银行","东亚银行","东亚银行","东亚银行","东亚银行","东亚银行","东亚银行",
"东亚银行",
"广东华兴银行","广东华兴银行",
"深圳农村商业银行","深圳农村商业银行","深圳农村商业银行",
"广州农村商业银行股份有限公司","广州农村商业银行股份有限公司","广州农村商业银行股份有限公司","广州农村商业银行股份有限公司",
"东莞农村商业银行","东莞农村商业银行","东莞农村商业银行",
"东莞农村商业银行","东莞农村商业银行",
"东莞市商业银行","东莞市商业银行",
"东莞市商业银行","东莞市商业银行",
"东莞市商业银行",
"广东省农村信用社联合社",
"广东省农村信用社联合社","广东省农村信用社联合社","广东省农村信用社联合社","广东省农村信用社联合社","广东省农村信用社联合社","广东省农村信用社联合社",
"大新银行","大新银行",
"大新银行","大新银行",
"大新银行","大新银行","大新银行","大新银行","大新银行","大新银行","大新银行",
"永亨银行","永亨银行","永亨银行","永亨银行","永亨银行","永亨银行","永亨银行","永亨银行",
"永亨银行","永亨银行","永亨银行","永亨银行",
"星展银行香港有限公司","星展银行香港有限公司",
"星展银行香港有限公司","星展银行香港有限公司","星展银行香港有限公司","星展银行香港有限公司",
"星展银行香港有限公司","星展银行香港有限公司","星展银行香港有限公司","星展银行香港有限公司","星展银行香港有限公司","星展银行香港有限公司","星展银行香港有限公司","星展银行香港有限公司",
"恒丰银行",
"恒丰银行","恒丰银行",
"天津市商业银行","天津市商业银行",
"天津市商业银行",
"天津市商业银行","天津市商业银行",
"天津滨海德商村镇银行",
"浙商银行","浙商银行","浙商银行",
"浙商银行","浙商银行","浙商银行","浙商银行","浙商银行","浙商银行","浙商银行","浙商银行","浙商银行",
"南洋商业银行","南洋商业银行","南洋商业银行","南洋商业银行","南洋商业银行","南洋商业银行","南洋商业银行",
"南洋商业银行","南洋商业银行",
"南洋商业银行","南洋商业银行","南洋商业银行","南洋商业银行","南洋商业银行",
"南洋商业银行","南洋商业银行","南洋商业银行","南洋商业银行",
"厦门银行","厦门银行",
"厦门银行",
"厦门银行","厦门银行",
"福建海峡银行",
"福建海峡银行","福建海峡银行",
"福建海峡银行",
"吉林银行","吉林银行",
"吉林银行",
"吉林银行","吉林银行","吉林银行",
"汉口银行",
"汉口银行","汉口银行","汉口银行",
"盛京银行",
"盛京银行","盛京银行",
"盛京银行","盛京银行",
"盛京银行","盛京银行",
"大连银行",
"大连银行","大连银行","大连银行","大连银行","大连银行",
"大连银行","大连银行","大连银行",
"河北银行","河北银行","河北银行","河北银行",
"河北银行","河北银行",
"乌鲁木齐市商业银行","乌鲁木齐市商业银行","乌鲁木齐市商业银行","乌鲁木齐市商业银行",
"乌鲁木齐市商业银行","乌鲁木齐市商业银行",
"绍兴银行",
"绍兴银行","绍兴银行","绍兴银行",
"绍兴银行",
"成都商业银行","成都商业银行","成都商业银行","成都商业银行","成都商业银行","成都商业银行","成都商业银行","成都商业银行","成都商业银行",
"抚顺银行",
"抚顺银行",
"抚顺银行","抚顺银行",
"郑州银行",
"郑州银行",
"郑州银行",
"宁夏银行","宁夏银行","宁夏银行","宁夏银行","宁夏银行",
"宁夏银行","宁夏银行","宁夏银行",
"重庆银行",
"重庆银行","重庆银行","重庆银行",
"哈尔滨银行","哈尔滨银行",
"哈尔滨银行",
"哈尔滨银行",
"兰州银行","兰州银行","兰州银行",
"兰州银行","兰州银行","兰州银行",
"青岛银行","青岛银行","青岛银行","青岛银行",
"青岛银行","青岛银行",
"秦皇岛市商业银行","秦皇岛市商业银行",
"秦皇岛市商业银行","秦皇岛市商业银行",
"青海银行","青海银行",
"青海银行","青海银行","青海银行",
"青海银行",
"台州银行",
"台州银行",
"台州银行",
"台州银行","台州银行",
"台州银行",
"长沙银行",
"长沙银行","长沙银行","长沙银行",
"长沙银行","长沙银行","长沙银行","长沙银行","长沙银行","长沙银行","长沙银行",
"长沙银行",
"泉州银行","泉州银行",
"泉州银行",
"泉州银行",
"泉州银行",
"包商银行","包商银行",
"包商银行",
"包商银行","包商银行","包商银行",
"龙江银行","龙江银行","龙江银行","龙江银行",
"龙江银行",
"龙江银行",
"龙江银行","龙江银行",
"上海农商银行","上海农商银行","上海农商银行",
"上海农商银行",
"上海农商银行","上海农商银行","上海农商银行","上海农商银行",
"中国建设银行",
"中国建设银行","中国建设银行","中国建设银行",
//"浙江泰隆商业银行",
//"浙江泰隆商业银行","浙江泰隆商业银行","浙江泰隆商业银行",
"内蒙古银行","内蒙古银行",
"内蒙古银行",
"广西北部湾银行",
"广西北部湾银行",
"桂林银行",
"桂林银行",
"桂林银行",
"龙江银行","龙江银行","龙江银行","龙江银行",
"龙江银行",
"龙江银行","龙江银行",
"龙江银行",
"龙江银行","龙江银行","龙江银行","龙江银行","龙江银行",
"成都农村商业银行",
"成都农村商业银行","成都农村商业银行","成都农村商业银行","成都农村商业银行",
"福建省农村信用社联合社","福建省农村信用社联合社","福建省农村信用社联合社","福建省农村信用社联合社","福建省农村信用社联合社","福建省农村信用社联合社",
"福建省农村信用社联合社","福建省农村信用社联合社","福建省农村信用社联合社",
"天津农村商业银行","天津农村商业银行",
"天津农村商业银行","天津农村商业银行",
"江苏省农村信用社联合社","江苏省农村信用社联合社","江苏省农村信用社联合社","江苏省农村信用社联合社",
"江苏省农村信用社联合社","江苏省农村信用社联合社","江苏省农村信用社联合社",
"湖南农村信用社联合社","湖南农村信用社联合社","湖南农村信用社联合社","湖南农村信用社联合社",
"江西省农村信用社联合社",
"江西省农村信用社联合社",
"商丘市商业银行",
"商丘市商业银行",
"华融湘江银行","华融湘江银行",
"华融湘江银行",
"衡水市商业银行","衡水市商业银行",
"重庆南川石银村镇银行",
"湖南省农村信用社联合社","湖南省农村信用社联合社","湖南省农村信用社联合社","湖南省农村信用社联合社",
"邢台银行","邢台银行",
"临汾市尧都区农村信用合作联社","临汾市尧都区农村信用合作联社",
"东营银行",
"东营银行",
"上饶银行",
"上饶银行",
"德州银行",
"德州银行",
"承德银行",
"云南省农村信用社","云南省农村信用社",
"柳州银行","柳州银行","柳州银行",
"柳州银行","柳州银行",
"柳州银行",
"威海市商业银行",
"威海市商业银行",
"湖州银行",
"潍坊银行","潍坊银行",
"潍坊银行",
"赣州银行","赣州银行",
"赣州银行",
"日照银行",
"南昌银行","南昌银行",
"南昌银行",
"南昌银行",
"贵阳银行","贵阳银行",
"贵阳银行",
"贵阳银行",
"锦州银行","锦州银行",
"锦州银行",
"齐商银行","齐商银行",
"齐商银行",
"珠海华润银行","珠海华润银行",
"珠海华润银行",
"葫芦岛市商业银行","葫芦岛市商业银行",
"宜昌市商业银行",
"宜昌市商业银行",
"杭州银行","杭州银行","杭州银行",
"杭州银行","杭州银行","杭州银行","杭州银行",
"苏州市商业银行",
"辽阳银行","辽阳银行",
"洛阳银行","洛阳银行",
"焦作市商业银行",
"焦作市商业银行",
"镇江市商业银行","镇江市商业银行",
"法国兴业银行",
"大华银行",
"企业银行",
"华侨银行",
"恒生银行","恒生银行",
"恒生银行","恒生银行",
"恒生银行","恒生银行",
"恒生银行","恒生银行","恒生银行","恒生银行","恒生银行","恒生银行",
"临沂商业银行","临沂商业银行",
"烟台商业银行",
"齐鲁银行","齐鲁银行",
"齐鲁银行",
"BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司",
"BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司","BC卡公司",
"集友银行","集友银行",
"集友银行","集友银行",
"集友银行","集友银行","集友银行","集友银行","集友银行",
"集友银行","集友银行",
"大丰银行","大丰银行","大丰银行",
"大丰银行","大丰银行","大丰银行",
"大丰银行",
"大丰银行","大丰银行",
"AEON信贷财务亚洲有限公司","AEON信贷财务亚洲有限公司","AEON信贷财务亚洲有限公司",
"AEON信贷财务亚洲有限公司",
"澳门BDA","澳门BDA"


            };

        }













    }
}
