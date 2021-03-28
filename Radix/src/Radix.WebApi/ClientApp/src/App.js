import React, {Component} from 'react';
import {Route} from 'react-router';

import {Layout} from './pages/Shared/Layout';
import {Home} from './pages/Home/Home';

import './custom.css';

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route exact path="/" component={Home} />
                {/* <Route path="/counter" component={Counter} /> */}
            </Layout>
        );
    }
}
