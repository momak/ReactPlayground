import React, { Component } from 'react';

export class Home extends Component {
    displayName = Home.name

    render() {
        return (
            <div>
                <h2>InScale, ProOffice Task!</h2>
                <p>This is a possible solution to the task assigned for position: <b>.Net Developer</b>.</p>
                <ul>
                    <li>You first need to create a Database (script is provided for MSSQL server).</li>
                    <li>You should configure connection string in the appsettings.json.</li>
                    <li>The solution is created with Visual Studio 2017. The project is MVC.Core (2.1).</li>
                    <ul>
                        <li>The project template that is used is React with .Net Core</li>
                        <li>However only small part of the coding is done in React.js</li>
                    </ul>
                    <li>For database access is used ORM (Entity Framework Core), Database First method.</li>
                </ul>
            </div>
        );
    }
}
