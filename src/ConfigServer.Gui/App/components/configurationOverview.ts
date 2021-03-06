﻿import { Component, Input, OnInit } from '@angular/core';
import { UploadDataService } from '../dataservices/upload-data.service';
import { Router } from '@angular/router';
import { ConfigurationModelSummary } from '../interfaces/configurationSetSummary';


@Component({
    selector: 'config-overview',
    template: `
            <div class="item">
                <h5>{{config.displayName}}</h5>
                <p>{{config.description}}</p>
                <button type="button" (click)="downloadConfig(configurationSetId,config.id)">Download</button>
                <json-file-uploader [(csMessage)]="uploadMessage" (onUpload)="uploadConfig($event)"></json-file-uploader>
                <button type="button" (click)="goToConfig(configurationSetId,config.id)">Edit</button>
            </div>
`
})
export class ConfigurationOverviewComponent {
    @Input('csConfig')
    config: ConfigurationModelSummary;
    @Input('csClientId')
    clientId: string;
    @Input('csConfigurationSetId')
    configurationSetId: string;

    uploadMessage: string;
    constructor(private uploadService: UploadDataService, private router: Router) {

    }

    goToConfig(configurationSetId: string, configId: string): void {
        this.router.navigate(['/client', this.clientId, configurationSetId, configId]);
    }

    downloadConfig(configurationSetId: string, configId: string) {
        window.open('download/' + this.clientId + '/' + configurationSetId + '/' + configId + '.json');
    }

    uploadConfig(value: any): void {
        this.uploadService.postConfig(this.clientId, this.config.id, value)
            .then(success => {
                if (!success)
                    this.uploadMessage = 'Invalid Config';
            });
    }

}