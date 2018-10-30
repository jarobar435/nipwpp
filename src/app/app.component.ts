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
  private posts: Post[];
  public postTitle;
  public postBody;

  constructor(private httpService: HttpService) { }

  ngOnInit() {
    this.createPost();
    this.readPosts();
  }

  public createPost() {
    this.httpService.postPosts(this.post)
    .subscribe(console.log);
  }

  public readPosts() {
    this.httpService.getPosts()
    .subscribe((response:Post[]) => {
        this.posts = response
    });
  }

  public updatePost(id) {
    this.httpService.putPosts(id, this.post)
    .subscribe(console.log);
  }

  public deletePost(id) {
    this.httpService.deletePosts(id)
    .subscribe(console.log);
  }
};
