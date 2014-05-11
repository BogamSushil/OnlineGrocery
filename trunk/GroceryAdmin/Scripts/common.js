var getSiteUrl = function (pageUrl) {
    var obj = location.pathname.split("/");
    if (window.location.hostname == "localhost") {
        return pageUrl;
    }
    else {
        return "/" + obj[1] + pageUrl;
    }
};

var ResourceFile = {
    Resource: []
};

var Extensions = [".doc", ".docx", ".odt", ".rtf"];


var AlertsMessages = {
    DeactivateClient: {
        value: 'Are you sure you want to Disable this client?  If you choose Yes, all users of this client will not be able to login to VLE.'
    },
    DeleteClient:
    {
        value: 'Are you sure you want to Delete this client from the system.' +
        'There are Groups/Users/Content associated with this client and if you delete the client, all its associated Groups/Users/Content or content will be deleted'
    },
    ForgotEmailReq:
    {
        value: 'Enter your Email address.'
    },
    ForgotEmailFormot:
    {
        value: 'The Email address you entered is incorrect.'
    },
    ConfirmDate:
    {
        value: 'Are you sure you want to continue with this Expiration Date because the Date which is choose by you is one year after from today'
    },
    DeactivateGroup:
    {
        value: 'Are you sure you want to Disable this Group? If you choose Yes, all users of this Group will not be able to login to VLE.'
    },
    DeleteGroup:
    {
        value: 'Are you sure you want to Delete this Group from the system. There are Users and/or Content associated with this client and if you delete the Group, all its associated Users and Content will be deleted with all usage data'
    },
    DeactivateUser:
    {
        value: 'Are you sure you want to Disable this User? If you choose Yes, this user will not be able to login to VLE.'
    },
    DeleteUser:
    {
        value: 'Are you sure you want to Delete this User from the system? All usage related date for this user will be deleted.'
    },
    BulkInsertCSVFileReq:
    {
        value: 'Please select a .CSV file with to create users in bulk.'
    },
    BulkInsertCSVFileFormat:
    {
        value: 'Incorrect file format. Please select a .CSV file with to create users in bulk.'
    },
    DeactivateContent:
    {
        value: 'Are you sure you want to Disable this Content?  If you choose Yes, all users of this Content will not be able to login to VLE.'
    },
    DeleteContent:
    {
        value: 'Are you sure you want to Delete this Content from the system.\n   There are Groups/Users/Content associated with this Content and if you delete the Content, all its associated Groups/Users/Content or content will be deleted'
    },
    ContentUploadFileReq:
    {
        value: 'Please upload correct files'
    },
    ContentUploadProgramAgendaFile:
    {
        value: 'You may only use a ".pdf" document for Program Agenda. Please chose a ".pdf" file and try again.'
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
    UserNameLength: {
        value: 'User Name should be minimum 6 and maximum 25 characters.'
    }
}

var EmailSubject = {
    UpdatePassword: {
        value: 'Advantexe VLE | Password Retrieval'
    },
    WelcomeClientEmail: {
        value: 'Welcome to Advantexe VLE'
    }
}