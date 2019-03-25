namespace NgNet.IO
{
    #region <class - hFilter>
    public class FilterHelper
    {
        public const string All = "|*.*|所有文件|";

        #region video
        public const string Video_WindowsMedia = "|*.asf|Windows media 视频|*.wmv|Windows media video|";
        public const string Video_WindowsMedias = "|*.asf;*.wmv|Windows media video|";

        public const string Video_MPEG = "|*.m4v|MPEG4视频|*.mp4|MPEG4视频|*.mpe|MPEG视频|*.mpeg|MPEG视频|*.mpg|MPEG视频|";
        public const string Video_MPEGs = "|*.m4v;*.mp4;*.mpe;*.mpeg;*.mpg|MPEG视频|";

        public const string Video_Real = "|*.rm|Real视频|.rmvb|Real视频|";
        public const string Video_Reals = "|*.rm;*.rmvb|Real视频|";

        public const string Video_QuickTime = "|*.3gp|手机/随身听视频|*.3gpp|手机/随身听视频|*.mov|QuickTime视频|";
        public const string Video_QuickTimes = "|*.3gp;*.3gpp;*.mov|QuickTime视频|";

        public const string Video_Flash = "|*.flv|*ShockWavef video flash|";
        public const string Video_Flashs = "|*.flv|*ShockWavef video flash|";

        public const string Video = Video_MPEG + Video_Real + Video_WindowsMedia + Video_QuickTime + Video_Flash;
        public const string Videos = Video_MPEGs + Video_Reals + Video_WindowsMedias + Video_QuickTimes + Video_Flashs;
        public const string Video_All = "|*.asf;*.wmv;*.m4v;*.mp4;*.mpe;*.mpeg;*.mpg;*.rm;*.rmvb;*.3gp;*.3gpp;*.mov;*.flv|视频文件|";
        #endregion

        #region audio
        public const string Audio_WindowsMedia = "|*.wma|Windows Media 音频|";
        public const string Audio_WindowsMedias = "|*.wma|Windows Media 音频|";

        public const string Audio_Real = "|*.ra|Real音频|";
        public const string Audio_Reals = "|*.ra|Real音频|";

        public const string Audio_QuickTime = "|*.aif|Mac音频|*.aiff|Mac音频|*.amr|手机/随身听音频|";
        public const string Audio_QuickTimes = "|*.aif;*.aiff;*.amr|Mac音频|";

        public const string Audio_MPEG = "|*.aac|MPEG音频|*.m4a|MPEG音频|*.mp2|mp2/mpa音频|*.mp3|MPEG音频|*.mpa|MPEG音频|*.mpga|mp3音频|";
        public const string Audio_MPEGs = "|*.aac;*.m4a;*.mp2;*.mp3;*.mpa;*.mpga|MPEG音频|";

        public const string Audio_Ogg = "|*.ogg|OGG vorbis 音频|";
        public const string Audio_Oggs = "|*.ogg|OGG vorbis 音频|";

        public const string Audio_Flac = "|*.flac|Flac音频|";
        public const string Audio_Flacs = "|*.flac|Flac音频|";

        public const string Audio_Ape = "|*.ape|*.mac|APE无损音频|";
        public const string Audio_Apes = "|*.ape;*.mac|APE无损音频|";

        public const string Audio_Mid = "|*.mid|mid音频|*.midi|mid音频|*.rmi|mid音频|";
        public const string Audio_Mids = "|*.mid;*.midi;*.rmi|mid音频|";

        public const string Audio_Wav = "|*.wav|Windows 波形声音文件|";

        public const string Audio = Audio_Ape + Audio_Flac + Audio_Mid + Audio_MPEG + Audio_Ogg + Audio_QuickTime + Audio_Real + Audio_Wav + Audio_WindowsMedia;
        public const string Audios = Audio_Apes + Audio_Flacs + Audio_Mids + Audio_MPEGs + Audio_Oggs + Audio_QuickTimes + Audio_Reals + Audio_Wav + Audio_WindowsMedias;
        public const string Audio_All = "|*.wma;*.ra;*.aif;*.aiff;*.amr;*.aac;*.m4a;*.mp2;*.mp3;*.mpa;*.mpga;*.ogg;*.flac;*.ape;*.mac;*.mid;*.midi;*.rmi;*.wav|音频文件|";
        #endregion

        #region image
        public const string Image_Jpeg = "|*.jpg;*.jpeg|jpeg图片|";

        public const string Image_Bitmap = "|*.bmp|bmp位图图像|";

        public const string Image_Png = "|*.png|png图片|";

        public const string Image_All = Image_Jpeg + Image_Bitmap + Image_Png;
        #endregion

        #region code
        public const string Code_VB = "|*.vb|Visual Basic源文件|";
        public const string Code_CSharp = "|*.cs|Visual C#源文件|";
        #endregion

        #region document
        public const string DOC_PDF = "|*.pdf|Adobe PDF Document|";

        public const string DOC_DOC = "|*.doc|Microsoft Word 1997-2003 Document|";
        #endregion

        #region Text
        public const string Text = "|*.txt|纯文本文档|";
        public const string WebPage = "|*.htm;*.html|HTML网页文件|";
        #endregion

        #region method
        public static bool IsFilter(string filter)
        {
            return true;
        }

        public static string Test(string filter, string defaultValue)
        {
            if (IsFilter(filter))
                return filter;
            else
                return defaultValue;
        }
        #endregion
    }
    #endregion
}
