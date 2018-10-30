import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Post } from './post';


const API_URL = 'https://jsonplaceholder.typicode.com/posts'
@Injectable()
export class HttpService {

  constructor(private http: HttpClient) { }

  getPosts() {
    return this.http.get(API_URL);
  }

  postPosts(post: Post) {
    return this.http.post(API_URL, post);
  }

  putPosts(id:number, post: Post) {
    return this.http.put(`${API_URL}/${id}`, post);
  }

  deletePosts(id: number) {
    return this.http.delete(`${API_URL}/${id}`);
  }
}
