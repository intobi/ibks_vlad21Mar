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
import type { PriorityDto2 } from './PriorityDto2';
import {
    PriorityDto2FromJSON,
    PriorityDto2FromJSONTyped,
    PriorityDto2ToJSON,
    PriorityDto2ToJSONTyped,
} from './PriorityDto2';
import type { InstalledEnvironmentDto2 } from './InstalledEnvironmentDto2';
import {
    InstalledEnvironmentDto2FromJSON,
    InstalledEnvironmentDto2FromJSONTyped,
    InstalledEnvironmentDto2ToJSON,
    InstalledEnvironmentDto2ToJSONTyped,
} from './InstalledEnvironmentDto2';
import type { TicketTypeDto2 } from './TicketTypeDto2';
import {
    TicketTypeDto2FromJSON,
    TicketTypeDto2FromJSONTyped,
    TicketTypeDto2ToJSON,
    TicketTypeDto2ToJSONTyped,
} from './TicketTypeDto2';
import type { StatusDto2 } from './StatusDto2';
import {
    StatusDto2FromJSON,
    StatusDto2FromJSONTyped,
    StatusDto2ToJSON,
    StatusDto2ToJSONTyped,
} from './StatusDto2';
import type { UserDto2 } from './UserDto2';
import {
    UserDto2FromJSON,
    UserDto2FromJSONTyped,
    UserDto2ToJSON,
    UserDto2ToJSONTyped,
} from './UserDto2';

/**
 * 
 * @export
 * @interface TicketDto
 */
export interface TicketDto {
    /**
     * 
     * @type {number}
     * @memberof TicketDto
     */
    id?: number;
    /**
     * 
     * @type {string}
     * @memberof TicketDto
     */
    title?: string | null;
    /**
     * 
     * @type {string}
     * @memberof TicketDto
     */
    url?: string | null;
    /**
     * 
     * @type {string}
     * @memberof TicketDto
     */
    description?: string | null;
    /**
     * 
     * @type {string}
     * @memberof TicketDto
     */
    applicationName?: string | null;
    /**
     * 
     * @type {string}
     * @memberof TicketDto
     */
    stackTrace?: string | null;
    /**
     * 
     * @type {string}
     * @memberof TicketDto
     */
    device?: string | null;
    /**
     * 
     * @type {string}
     * @memberof TicketDto
     */
    browser?: string | null;
    /**
     * 
     * @type {string}
     * @memberof TicketDto
     */
    resolution?: string | null;
    /**
     * 
     * @type {Date}
     * @memberof TicketDto
     */
    date?: Date;
    /**
     * 
     * @type {Date}
     * @memberof TicketDto
     */
    lastModified?: Date;
    /**
     * 
     * @type {PriorityDto2}
     * @memberof TicketDto
     */
    priority?: PriorityDto2 | null;
    /**
     * 
     * @type {TicketTypeDto2}
     * @memberof TicketDto
     */
    ticketType?: TicketTypeDto2 | null;
    /**
     * 
     * @type {StatusDto2}
     * @memberof TicketDto
     */
    status?: StatusDto2 | null;
    /**
     * 
     * @type {UserDto2}
     * @memberof TicketDto
     */
    user?: UserDto2 | null;
    /**
     * 
     * @type {InstalledEnvironmentDto2}
     * @memberof TicketDto
     */
    installedEnvironment?: InstalledEnvironmentDto2 | null;
}

/**
 * Check if a given object implements the TicketDto interface.
 */
export function instanceOfTicketDto(value: object): value is TicketDto {
    return true;
}

export function TicketDtoFromJSON(json: any): TicketDto {
    return TicketDtoFromJSONTyped(json, false);
}

export function TicketDtoFromJSONTyped(json: any, ignoreDiscriminator: boolean): TicketDto {
    if (json == null) {
        return json;
    }
    return {
        
        'id': json['id'] == null ? undefined : json['id'],
        'title': json['title'] == null ? undefined : json['title'],
        'url': json['url'] == null ? undefined : json['url'],
        'description': json['description'] == null ? undefined : json['description'],
        'applicationName': json['applicationName'] == null ? undefined : json['applicationName'],
        'stackTrace': json['stackTrace'] == null ? undefined : json['stackTrace'],
        'device': json['device'] == null ? undefined : json['device'],
        'browser': json['browser'] == null ? undefined : json['browser'],
        'resolution': json['resolution'] == null ? undefined : json['resolution'],
        'date': json['date'] == null ? undefined : (new Date(json['date'])),
        'lastModified': json['lastModified'] == null ? undefined : (new Date(json['lastModified'])),
        'priority': json['priority'] == null ? undefined : PriorityDto2FromJSON(json['priority']),
        'ticketType': json['ticketType'] == null ? undefined : TicketTypeDto2FromJSON(json['ticketType']),
        'status': json['status'] == null ? undefined : StatusDto2FromJSON(json['status']),
        'user': json['user'] == null ? undefined : UserDto2FromJSON(json['user']),
        'installedEnvironment': json['installedEnvironment'] == null ? undefined : InstalledEnvironmentDto2FromJSON(json['installedEnvironment']),
    };
}

export function TicketDtoToJSON(json: any): TicketDto {
    return TicketDtoToJSONTyped(json, false);
}

export function TicketDtoToJSONTyped(value?: TicketDto | null, ignoreDiscriminator: boolean = false): any {
    if (value == null) {
        return value;
    }

    return {
        
        'id': value['id'],
        'title': value['title'],
        'url': value['url'],
        'description': value['description'],
        'applicationName': value['applicationName'],
        'stackTrace': value['stackTrace'],
        'device': value['device'],
        'browser': value['browser'],
        'resolution': value['resolution'],
        'date': value['date'] == null ? undefined : ((value['date']).toISOString()),
        'lastModified': value['lastModified'] == null ? undefined : ((value['lastModified']).toISOString()),
        'priority': PriorityDto2ToJSON(value['priority']),
        'ticketType': TicketTypeDto2ToJSON(value['ticketType']),
        'status': StatusDto2ToJSON(value['status']),
        'user': UserDto2ToJSON(value['user']),
        'installedEnvironment': InstalledEnvironmentDto2ToJSON(value['installedEnvironment']),
    };
}

