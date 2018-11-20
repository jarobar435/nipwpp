import { HttpService } from 'app/http.service';
import { Component, OnInit } from '@angular/core';
import { Post } from 'app/post';
import { Router } from '@angular/router';

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  styleUrls: ['./post-list.component.css']
})
export class PostListComponent implements OnInit {
  private post: Post = { title: 'SomeTitle', description: 'SomeDescription' };
  posts: Post[];
  public postTitle;

  constructor(private httpService: HttpService, private router: Router) { }

  ngOnInit() {
    this.readPosts();
  }

  public readPosts() {
    this.httpService.getPosts()
    .subscribe((response:Post[]) => {
        this.posts = response;
    });
  }

  public createPost() {
    this.httpService.postPosts(this.post)
    .subscribe(console.log);
  }

  public updatePost(id) {
    this.httpService.putPosts(id, this.post)
    .subscribe(console.log);
  }

  public deletePost(id) {
    this.httpService.deletePosts(id)
    .subscribe(console.log);
  }

  handleUpdate(id: number) {
    this.updatePost(id);
  }

  handleDelete(id: number) {
    this.deletePost(id);
  }

  handleAddPost() {
    this.router.navigate(['add-post']);
  }
}
