import { HttpService } from './http.service';
import { Component, OnInit } from '@angular/core';
import { Post } from './post';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  private post: Post = { userId: 5, id: 101, title: 'SomeTitle', body: 'somebody' };
  private post2: Post;
  public postTitle;
  public postBody;

  constructor(private httpService: HttpService) { }

  ngOnInit() {
    this.createPost();
    this.insertData();
  }

  public createPost() {
    this.httpService.postPosts(this.post)
    .subscribe(console.log);
  }

  public readPost() {
    this.httpService.getPosts()
    .subscribe(response => {
        this.post2 = response as Post;
    });
  }

  public updatePost() {
    this.httpService.postPosts(this.post)
    .subscribe(console.log);
  }

  public deletePost() {
    this.httpService.postPosts(this.post)
    .subscribe(console.log);
  }

  public insertData() {
    this.postTitle = (`${this.post.title}`);
    this.postBody = (`${this.post.body}`);
  }
};
