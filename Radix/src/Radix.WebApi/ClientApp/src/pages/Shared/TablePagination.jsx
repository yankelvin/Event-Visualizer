import React from 'react';
import BootstrapTable from 'react-bootstrap-table-next';
import paginationFactory from 'react-bootstrap-table2-paginator';

import 'react-bootstrap-table-next/dist/react-bootstrap-table2.min.css';
import 'react-bootstrap-table2-paginator/dist/react-bootstrap-table2-paginator.min.css';

export default function TablePagination(props) {
    return (
        <BootstrapTable
            keyField="text"
            data={props.data}
            columns={props.columns}
            pagination={paginationFactory()}
        />
    );
}
