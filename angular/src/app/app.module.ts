import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { HttpService } from 'app/http.service';
import { HttpClientModule } from '@angular/common/http';
import { PostListComponent } from './post-list/post-list.component';
import { PostComponent } from './post/post.component';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { RouterModule, Routes } from '@angular/router';

const appRoutes: Routes = [
  {
    path: 'add-post',
    component: PostComponent
  }
];

@NgModule({
  declarations: [
    AppComponent,
    PostListComponent,
    PostComponent
  ],
  imports: [
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: true }
    ),
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    TableModule,
    ButtonModule,
    FormsModule,
    InputTextModule
  ],
  providers: [
    HttpService
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
