using System;
using System.Collections.Generic;
using System.Text;

///Keep these local copies of COM types up to date with the original. They can be used
///to avoid direct dependence on COM DLLs.


namespace DVDVideoSoft.RockId_EXTERN
{
    public enum RockIdErrorCode
    {
        Success = 0,
        Fail,
        CantDecodeFile,	/*Cant decode media file*/
        DurationTooShort,	/*Short duration of music*/
        FPCodegenError,	/*Fingerprint codegen error*/
        FPServerError,	/*Fingerprint server error*/
        FPNotFound,	/*Fingerprint not found on server*/
        LFMServerError,	/*Last FM server error, look at log*/
        LFMNotFound,	/*Last FM server error, look at log*/
        HttpError,	/*Http error*/
        MBServerError,	/*Http error*/
        MBNotFound,	/*Http error*/
        InternalError,	/*Http error*/
        //kRockIdErr_MyError = kRockIdErr_HttpError + kHttpErr_Max,	/*Http error*/
    }

    public enum RockIdEntryIds
    { 
        Artist,
        Track,
        Release,
        TrackNumber,
        ReleaseGenre,
        ReleaseArtworkUrl
    }
}
