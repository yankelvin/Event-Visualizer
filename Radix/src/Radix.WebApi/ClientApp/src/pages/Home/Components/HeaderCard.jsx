import React from 'react';

export default function HeaderCard(props) {
    return (
        <div className="card region-card info-card col-md-2 mt-4 mb-4">
            <div>
                <div>
                    <b>{props.title}</b>
                </div>
                <span>{props.text}</span>
            </div>
        </div>
    );
}
