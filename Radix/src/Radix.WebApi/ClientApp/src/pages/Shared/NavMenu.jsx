import React, {Component} from 'react';

import {
    Collapse,
    Container,
    Navbar,
    NavbarBrand,
    NavbarToggler,
    NavItem,
    NavLink,
} from 'reactstrap';

import {Link} from 'react-router-dom';
import './NavMenu.css';

export class NavMenu extends Component {
    static displayName = NavMenu.name;

    constructor(props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.state = {
            collapsed: true,
        };
    }

    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed,
        });
    }

    render() {
        return (
            <header>
                <Navbar
                    className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3"
                    light
                >
                    <Container>
                        <NavbarBrand tag={Link} to="/">
                            Radix - Leitura de Sensores
                        </NavbarBrand>
                        <NavbarToggler
                            onClick={this.toggleNavbar}
                            className="mr-2"
                        />
                        <Collapse
                            className="d-sm-inline-flex flex-sm-row-reverse"
                            isOpen={!this.state.collapsed}
                            navbar
                        >
                            <ul className="navbar-nav flex-grow">
                                <NavItem>
                                    <a
                                        className="text-dark"
                                        href="https://github.com/yankelvin"
                                    >
                                        GitHub
                                    </a>
                                </NavItem>

                                <NavItem className="ml-3">
                                    <a
                                        className="text-dark"
                                        href="https://www.linkedin.com/in/yan-kelvin-313b3b160/"
                                    >
                                        LinkedIn
                                    </a>
                                </NavItem>
                            </ul>
                        </Collapse>
                    </Container>
                </Navbar>
            </header>
        );
    }
}
