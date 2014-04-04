function GlobalDialog(Title, Message) {
    $('#divGlobalErrors').dialog({
        title: Title,
        width: 400,
        height: 250,
        resizable: true,
        modal: true,
        open: function (event, ui) {
            $("#divErrorMessage").html(Message);
        }
    });
}

var AlertsMessages = {
    Delete_eBook:
    {
        value: 'Are you sure you want to Delete this Document from the system?'
    },
    Delete_MP3:
    {
        value: 'Are you sure you want to Delete this cast from the system?'
    },
    DeleteQuestion:
    {
        value: 'Are you sure you want to Delete this Questions from the system?'
    },
    DeleteMultipleQuestion:
    {
        value: 'Are you sure you want to Delete selected Questions from the system?'
    },
    DeleteMultipleWord:
    {
        value: 'Are you sure you want to Delete selected words from the system?'
    },
    SelectQuestions:
   {
       value: 'No questions are selected, please select question first to mass delete'
   },
    SelectWords:
   {
       value: 'No words are selected, please select word first to mass delete'
   },
    UnapproveUser:
    {
        value: 'Are you sure you want to Unapprove this User?'
    },
    ApproveUser:
    {
        value: 'Are you sure you want to Approve this User?'
    },
    UnapproveMultipleUser:
    {
       value: 'Are you sure you want to Unapprove selected Users?'
    },
    ApproveMultipleUser:
    {
        value: 'Are you sure you want to Approve selected Users?'
    },
    SelectUsersForAction:
    {
          value: 'No users are selected, please select users first'
    },
    NewPasswordEmail:
    {
        value: 'Password has been successfully send on users register Email Address.'
    },
    WelcodeEmail:
    {
        value: 'User Welcome email has been successfully send on users register Email Address.'
    },
    DeleteVideo:
    {
        value: 'Are you sure you want to Delete this Video from the system?'
    },
    DeleteExam:
    {
       value: 'Are you sure you want to Delete this challenge series from the system?'
    },
    BulkInsertCSVFileReq:
    {
        value: 'Please select a .CSV file.'
    },
    BulkInsertCSVFileFormat:
    {
        value: 'Incorrect file format. Please select a .CSV file.'
    },
    ContentUploadeBookDocFile:
    {
        value: 'Select ".docx" or ".doc" file type and try again.'
    },
    ContentUploadeBookPdfFile:
    {
        value: 'Select ".pdf" file type and try again.'
    },
    ContentUploadJPGdfFile:
    {
        value: 'Select ".jpg" file type and try again.'
    },
    ContentUploadMP3File:
    {
        value: 'Select ".mp3" file type and try again.'
    },
    ContentUploadFileLength:
    {
        value: 'Please upload file with proper length as 150 Characters only.'
    },
    UserProfilePasswordLength:
    {
        value: 'Password has to be at least 6 Characters long.'
    },
    UserProfileConfirmPassowrd:
    {
        value: 'Confirm New Password does not match with New Password. Please correct and try again.'
    },
    UserProfileOldNewPassword:
    {
        value: 'New and old password should not be same.'
    },
    UserProfileOldNewConfirmPassReq:
    {
        value: 'Old, New and Confirm password are required.'
    },
    UserProfileOldPassReq:
    {
        value: 'Old Password is required.'
    },
    UserProfileNewPassReq:
    {
        value: 'New Password is required.'
    },
    UserProfileConfirmPassReq:
    {
        value: 'Confirm Password is required.'
    },
    ForgotPasswordConfirm:
    {
        value: 'Password would be send to valid registered email address, please check your email.'
    },
    ServerError:
    {
        value: 'Server Error. Please contact administrator.'
    },
    ForgotEmailReq:
   {
       value: 'Enter your Email address.'
   },
    ForgotEmailFormot:
    {
        value: 'The Email address you entered is incorrect.'
    },
    ContentTypeForVideo:
    {
        value: 'Select ".mp4" file type and try again.'
    },
    UserNameNotAvailable:
    {
        value: 'User Name already in use.'
    },
    UserNameAvailable:
    {
        value: 'User Name is available.'
    }
}
var EmailSubject = {
    UpdatePassword: {
        value: 'O and M | Password Retrieval'
    },
    WelcomeUserEmail: {
        value: 'Welcome to O and M'
    }
}