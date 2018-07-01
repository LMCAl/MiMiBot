import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { MatButtonModule, MatProgressBarModule } from '@angular/material';
import {MatRadioModule} from '@angular/material/radio';
import {MatInputModule} from '@angular/material/input';
import {MatCardModule} from '@angular/material/card';
import { MatMenuModule } from '@angular/material';
import { MatTabsModule } from '@angular/material';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './home/home/home.component';
import { RouterModule, Routes } from '@angular/router';
import { TokenService } from 'src/app/login/token.service';
import {MatIconModule} from '@angular/material/icon';
import { DashboardComponent } from './analytics/dashboard/dashboard.component';
import { ChartsModule } from 'ng2-charts';
import { TopicDetailsComponent } from './analytics/dashboard/modules/topic-details/topic-details.component';
import { TagCloudModule } from 'angular-tag-cloud-module';
import { UploadsComponent } from './personalize/uploads/uploads.component';
import { EmotionAnalysisComponent } from './analytics/dashboard/modules/emotion-analysis/emotion-analysis.component';
import { EmotionDetailsComponent } from './analytics/dashboard/modules/emotion-details/emotion-details.component';
import { MatTableModule } from '@angular/material/table';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatSortModule } from '@angular/material/sort'
import {MatPaginatorModule} from '@angular/material/paginator';
import { IndividualDetailsComponent } from './analytics/dashboard/modules/individual-details/individual-details.component'
import {MatDividerModule} from '@angular/material/divider';
import { KeywordsDetailsComponent } from './analytics/dashboard/modules/keywords-details/keywords-details.component';
import {MatSelectModule} from '@angular/material/select';
import { BlobModule } from 'angular-azure-blob-service';
import { DashboardOverviewComponent } from './analytics/dashboard/modules/dashboard-overview/dashboard-overview.component';
import {MatGridListModule} from '@angular/material/grid-list';
const appRoutes: Routes = [
  { path: '', redirectTo: '/login' ,  pathMatch:'full' },
  { path:'login' , component: LoginComponent },
  { path:'home' , component: HomeComponent , children : [
    { path: '' , component: DashboardComponent , outlet:"content"},
    { path:'dashboard' , component: DashboardComponent , outlet:"content" },
    { path:'upload' , component: UploadsComponent , outlet: "content"},
  ]},
 

]

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    DashboardComponent,
    TopicDetailsComponent,
    UploadsComponent,
    EmotionAnalysisComponent,
    EmotionDetailsComponent,
    IndividualDetailsComponent,
    KeywordsDetailsComponent,
    DashboardOverviewComponent,
    
  ],
  imports: [
    BrowserModule,
    MatMenuModule, 
    MatButtonModule, 
    MatTabsModule, 
    BrowserAnimationsModule, 
    MatFormFieldModule, 
    MatProgressBarModule,
    MatRadioModule,
    MatInputModule,
    FormsModule,
    HttpClientModule,
    RouterModule,
    RouterModule.forRoot(
    appRoutes,),
    MatIconModule, 
    ChartsModule,
    MatCardModule, 
    TagCloudModule,
    MatExpansionModule,
    MatTableModule,
    MatSortModule,
    MatDividerModule,
    MatPaginatorModule,
    MatSelectModule,
    BlobModule.forRoot(),
    MatGridListModule
  ],
  providers: [TokenService],
  bootstrap: [AppComponent]
})
export class AppModule { }
