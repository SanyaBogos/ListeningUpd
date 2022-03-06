namespace Listening.Core
{
    public enum ExternalLoginStatus
    {
        Ok = 0, Error, Invalid, TwoFactor, Lockout, CreateAccount
    }

    public enum FileContentType : byte
    {
        Audio = 0, Video, Captcha, StegPict, StegAud, StegVid, BlogVid
    }
}