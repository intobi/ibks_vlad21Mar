/* tslint:disable */
/* eslint-disable */
/**
 * TicketTracking.Api | v1
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0.0
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */

import { mapValues } from '../runtime';
/**
 * 
 * @export
 * @interface InstalledEnvironmentDto2
 */
export interface InstalledEnvironmentDto2 {
    /**
     * 
     * @type {number}
     * @memberof InstalledEnvironmentDto2
     */
    id?: number;
    /**
     * 
     * @type {string}
     * @memberof InstalledEnvironmentDto2
     */
    title?: string | null;
}

/**
 * Check if a given object implements the InstalledEnvironmentDto2 interface.
 */
export function instanceOfInstalledEnvironmentDto2(value: object): value is InstalledEnvironmentDto2 {
    return true;
}

export function InstalledEnvironmentDto2FromJSON(json: any): InstalledEnvironmentDto2 {
    return InstalledEnvironmentDto2FromJSONTyped(json, false);
}

export function InstalledEnvironmentDto2FromJSONTyped(json: any, ignoreDiscriminator: boolean): InstalledEnvironmentDto2 {
    if (json == null) {
        return json;
    }
    return {
        
        'id': json['id'] == null ? undefined : json['id'],
        'title': json['title'] == null ? undefined : json['title'],
    };
}

export function InstalledEnvironmentDto2ToJSON(json: any): InstalledEnvironmentDto2 {
    return InstalledEnvironmentDto2ToJSONTyped(json, false);
}

export function InstalledEnvironmentDto2ToJSONTyped(value?: InstalledEnvironmentDto2 | null, ignoreDiscriminator: boolean = false): any {
    if (value == null) {
        return value;
    }

    return {
        
        'id': value['id'],
        'title': value['title'],
    };
}

