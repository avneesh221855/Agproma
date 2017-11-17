//module declaration
import { BrowserModule } from '@angular/platform-browser';
import { SocialLoginModule } from "angular4-social-login";
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { DndModule } from 'ng2-dnd';
import { NgxPaginationModule } from 'ngx-pagination';
import { Http, HttpModule } from '@angular/http';
import { ShowHidePasswordModule } from 'ngx-show-hide-password';
import { LineGraphComponent} from './user-profile/line-graph/line-graph.component'
import { ChartsModule} from 'ng2-charts'
import {TruncateModule} from 'ng2-truncate';

//component declaration
import { SignupComponent } from './signup/signup.component';
import { RegisterComponent } from './register/register.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ProjectScreenComponent } from './project-screen/project-screen.component';
import { ProjectDetailComponent } from './project-screen/project-detail/project-detail.component';
import { FillDetailsComponent } from './project-screen/fill-details/fill-details.component';
import { ForgetPasswordComponent } from './forget-password/forget-password.component';
import { RegisterUserWithNewPasswordComponent } from './register-user-with-new-password/register-user-with-new-password.component';
import { InvitePeopleComponent } from './invite-people/invite-people.component';
import { TeamsComponent } from './teams/teams.component';
import { EpicComponent } from './epic/epic.component';
import { BacklogComponent } from './backlog/backlog.component';
import { ReleasePlanComponent } from './release-plan/release-plan.component';
import { SprintComponent } from './sprint/sprint.component';
import { ChecklistComponent } from './checklist/checklist.component';
import { TaskAddComponent } from './taskadd/taskadd.component';
import { TaskAssignComponent } from './taskAssign/taskAssign.component';
import { LandingpageComponent } from './landingpage/landingpage.component';
import { DashboardRoleComponent } from './dashboard-role/dashboard-role.component';
import { EfficiencyGraphComponent } from './user-profile/efficiency-graph/efficiency-graph.component';
import { KanbanBoardComponent } from './kanban-board/kanban-board.component';
import { UserProfileComponent} from "./user-profile/user-profile.component";

//service declaration
import { LoginService } from "./shared/services/login.service";
import { InvitePeopleService } from "./shared/services/invite-people.service";
import { ForgetServiceService } from "./shared/services/forget-service.service";
import { RegisterUserWithNewPasswordService } from "./shared/services/register-user-with-new-password.service";
import { AuthService } from "angular4-social-login";
import { AuthServiceConfig, GoogleLoginProvider, FacebookLoginProvider } from 'angular4-social-login';
import { ProjectScreenService } from './shared/services/project-screen.service';
import { ChecklistService } from './shared/services/checklist.service';
import { KanbanService } from "./shared/services/kanban.service";
import { EfficiencyGraphService } from "./shared/services/efficiency-graph.service";
import { ErrorComponent } from './shared/component/error/error.component';
import { BurndownService } from './shared/services/burndown.service';




//configuration for social  login
let config = new AuthServiceConfig([
  {
    id: GoogleLoginProvider.PROVIDER_ID,
    provider: new GoogleLoginProvider("861775280808-se6g8tlmjg61rqc97mbi29cdrobfj4ed.apps.googleusercontent.com")
  },
  {
    id: FacebookLoginProvider.PROVIDER_ID,
    provider: new FacebookLoginProvider("362127810880411")
  }
]);

export function provideConfig() {
  return config;
}

@NgModule({
  declarations: [
    LandingpageComponent,
    TaskAssignComponent,
    TaskAddComponent,
    ChecklistComponent,
    EpicComponent,
    AppComponent,
    SignupComponent,
    RegisterComponent,
    DashboardComponent,
    ProjectScreenComponent,
    ProjectDetailComponent,
    FillDetailsComponent,
    ForgetPasswordComponent,
    RegisterUserWithNewPasswordComponent,
    InvitePeopleComponent,
    TeamsComponent,
    BacklogComponent,
    ErrorComponent,
    ReleasePlanComponent,
    SprintComponent,
    LineGraphComponent, 
    KanbanBoardComponent,
    EfficiencyGraphComponent,
    DashboardRoleComponent,
    ErrorComponent,
    UserProfileComponent
  ],
  imports: [
    DndModule.forRoot(),
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    NgxPaginationModule,
    HttpModule,
    TruncateModule,
    ShowHidePasswordModule.forRoot(),ChartsModule
   ,
    ShowHidePasswordModule.forRoot()
  ],
  providers: [
    KanbanService,
    ChecklistService,
    ProjectScreenService,
    AuthService,
    ForgetServiceService,
    InvitePeopleService,
    RegisterUserWithNewPasswordService,
    LoginService,
    BurndownService,
    EfficiencyGraphService,
    {
      provide: AuthServiceConfig,
      useFactory: provideConfig
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
