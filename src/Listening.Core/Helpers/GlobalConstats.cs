
using System;

namespace Listening.Server
{
    public class GlobalConstats
    {
        public const string NOT_ALLOWED_TEXT = "You are not allowed to do this operation";
        public const string UNABLE_TO_CHANGE_PASSWORD = "Unable to change password";
        public const string INVALID_USER_CREDENTIALS = "Your credentials do not match or account not enabled. Please try again.";
        public const string INVALID_LOGIN_ATTEMPT = "Invalid login attempt.";
        public const string INSUFFICIENT_SMS_CREDIT_ERROR_MESSAGE = "Your SMS credit is not enought to process the request.";
        //public const int DefaultYear = 2018;
        public static readonly DateTime DefaultDate = new DateTime(2018, 1, 1);

        // text
        public const string AUDIO_OR_VIDEO_IS_NECESSARY = "audio_or_video_is_necessary";
        public const string NOT_ALLOWED_TO_USE_BOTH_AUDIO_AN_VIDEO = "not_allowed_to_use_both_audio_an_video";

        // file
        public const string FROM_IS_NOT_TO = "from_is_not_to";
        public const string MAX_DURATION_EXCEEDS = "max_duration_exceeds";

        // account
        public const string CAPTCHA_HASH_KEY = "CV";
        public const string CAPTCHA_SPLITTER = "_";

        public const string CREATED_NAME = "app_created";
        public const string UPDATED_NAME = "app_updated";
        public const string TEXTS_MODIFIED = "txts_upd";

        public const string USER = "User";
        public const string MODERATOR = "Moderator";
        public const string ADMIN = "Admin";
        public const string SUPER = "Super";
        public const string SPECIFIC = "Specific";
        public const string SPECADM = "SpecAdm";

        public const string STEG_TOO_HUGE_MESSAGE = "spec_too_huge_msg";
        public const string STEG_EJECT_INCREASE_LENGTH = "spec_ej_incr_lngth";
        public const string STEG_NOT_ENOUGH_POINTS= "steg_no_engh_points";

    }
}