import { HttpClient } from '@angular/common/http';
import { error } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';

export class Message {
  content: string;
  author: string;
}

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css']
})
export class TestComponent implements OnInit {

  backendResponse: string;

  constructor(private http: HttpClient) { }

  ngOnInit() {
  }
  sendRequestToBackend() {

    var message = new Message();
    message.content = "jakasWiadomosc";
    message.author = "Lukasz";
    this.http.post("https://localhost:44371/" + "Course" + "/sendMessage", message).subscribe(response => {
      this.backendResponse = (response as any).content;
    },
      error => {
        this.backendResponse = error;
      });
  }
}
