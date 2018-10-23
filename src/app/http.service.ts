import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Post } from './post';

@Injectable()
export class HttpService {

  constructor(private http: HttpClient) { }

  getPosts() {
    return this.http.get('https://angular.io/guide/http');
  }

  postPosts(post: Post) {
    return this.http.post('https://angular.io/guide/http', post);
  }

  putPosts(post: Post) {
    return this.http.put('https://angular.io/guide/http', post);
  }

  deletePosts(post: Post) {
    return this.http.delete('https://angular.io/guide/http');
  }
}
