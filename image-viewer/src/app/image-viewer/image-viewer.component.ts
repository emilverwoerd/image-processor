import { Component, OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';

@Component({
  selector: 'app-image-viewer',
  templateUrl: './image-viewer.component.html',
  styleUrls: ['./image-viewer.component.scss']
})
export class ImageViewerComponent {

  private hubConnection: HubConnection;
  public currentImg:string;

  constructor() { 
    // Create connection
    this.hubConnection = new HubConnectionBuilder()
      .withUrl("http://localhost:7071/api")
      .build();

    // Start connection. This will call the negotiate endpoint
    this.hubConnection
      .start();

    //Handle incoming events for the specific target
    this.hubConnection.on("newImage", (event) => {
      console.log(event)
      this.currentImg = event.Url;
    });
  }
}
