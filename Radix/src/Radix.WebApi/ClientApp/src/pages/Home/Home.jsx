import './Home.css';
import React, {Component} from 'react';

import EventHub from './Components/EventHub';

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
