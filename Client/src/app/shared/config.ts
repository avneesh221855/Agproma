export const ConfigFile =
{
    ProjectMasterUrls: {
        getAllProjectOfEmployee: "http://localhost:52258/api/ProjectMaster/",
        addNewProject: "http://localhost:52258/api/ProjectMaster",
        deleteProject: "http://localhost:52258/api/ProjectMaster/",
        updateProject: "http://localhost:52258/api/ProjectMaster/",
        getProjectData: "http://localhost:52258/api/ProjectMaster/GetProjectData/"
    },
    BacklogUrls: {
        connection: 'http://192.168.252.125:8030/backlog'
    },
    SprintUrls: {
        connection: 'http://localhost:52258/sprint'
    },
    KanBanUrls:{
        getTaskUrl : 'http://localhost:52258/api/TaskBacklog/GetAllTaskDetail/',
        getProjectData: 'http://localhost:52258/api/Burndown/GetProjectData/',
        GetSprintDetails:"http://localhost:52258/api/Burndown/GetSprintDetails/",
        GetVelocityDetails:"http://localhost:52258/api/Burndown/GetVelocityDetails/"
    },
    TeamUrls:{
        getTeamUrl:'http://localhost:52258/teamhub'
    },
    TaskAssignUrls:{
        connection:'http://localhost:52258/taskbacklog'
    },
    TaskAddUrls:{
        connection:'http://localhost:52258/taskhub'
    },
    ReleasePlanUrls:{
        connection:'http://localhost:52258/releaseplan',
        navigateNewRrelease:'/app-dashboard/newreleasedetail/1'
    },
    ProductBacklog:{
        connection:'http://localhost:52258/backlog'
    },
    DashboardUrls:{
        onLoggedOut:'app-signup/:id'
    },
    DashboardRoleUrls:{
        onLoggedOut:'app-signup/:id'
    },
    EpicUrls:{
        connection:'http://localhost:52258/epichub'
    },
    FillDetailsUrls:{
        backOnPrevious:'/app-dashboard/project-screen',
        dashboardNavigation:'app-dashboard'
    },
    ProjectDetailUrls:{
        navigation:'role-dashboard'
    },
    RegisterUrls:{
        registerNavigationById:'app-register/:id',
        dashboardNavigation:'app-dashboard',
        signupNavigation:'/app-signup',
        signupNavigationById:'/app-signup/:id'
    },
    RegisterUserWithNewPasswordUrls:{
        signupNavigation:'/app-signup/:id'
    },
    ActualEndDate: "10/10/1970",
    ChecklistServiceUrl:{
        getTaskUrl:"http://localhost:52258/api/Checklist/GetTaskDetail/",
        checkListUrl:'http://localhost:52258/api/Checklist/',
        efficiencyUrl:'http://localhost:52258/api/Efficiency/',
        updateDailyStatus:'http://localhost:52258/api/Checklist/updateDailyStatus/'
    },
    BurndownServiceUrl:{
        burndownUrl:'http://localhost:52258/api/Burndown/GetTasks/'
    },
    EfficiencyService:{
        efficiencyurl:'http://localhost:52258/api/Efficiency/'
    },
    ForgetServiceUrl:{
       forgeturl:"http://localhost:52258/api/forgetpassword?email="
    },
    InvitePeopleServiceUrl:{
        invite_url:'http://localhost:52258/api/InviteMembers' //used for calling Invite People controller in API
    },
    LoginServiceUrl:{
        url: 'http://localhost:52258/api/Login',                //url for login 
        memberUrl:'http://localhost:52258/api/ProjectMember',   //url for project members 
        invite_url:'http://localhost:52258/api/InviteMembers/',
        checkurl:'http://localhost:52258/api/Login/Check',
        detailUrl:'http://localhost:52258/api/Login/Details/',
        updatePasswordUrl:'http://localhost:52258/api/Login/UpdatePassword/',
        logouturl:"http://localhost:52258/api/Login/SetLogOut/",
        getToken: 'http://localhost:59382/api/TokenGeneration/createtoken',
        getTokenForFbandGoogle:'http://localhost:59382/api/TokenGeneration/createtokenforfbandgoogle/'
    },
    RegisterUserServiceUrls:{
        registernewpassword:"http://localhost:52258/api/Master/"
    }
}