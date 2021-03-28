import React, {Component} from 'react';

import EventHub from './EventHub';

export class Home extends Component {
    static displayName = Home.name;

    render() {
        return (
            <div>
                <EventHub />
            </div>
        );
    }
}
