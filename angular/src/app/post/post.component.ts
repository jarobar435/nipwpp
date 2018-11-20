import { HttpService } from 'app/http.service';
import { Component, OnInit } from '@angular/core';
import { Post } from 'app/post';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {
  private post: Post = { title: 'SomeTitle', description: 'SomeDescription' };

  constructor(private httpService: HttpService) { }

  ngOnInit() {
    console.log('jestem w dsflkasj');
  }
}
