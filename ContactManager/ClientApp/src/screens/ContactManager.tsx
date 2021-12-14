import { useEffect, useState } from 'react';
import Contact from '../api/contracts/Contact';
import { getContacts } from '../api/Methods';
import { DataGrid, GridToolbar } from '@mui/x-data-grid';

export default function ContactManager() {
  const [contacts, setContacts] = useState<Contact[]>([]);
  useEffect(() => {
    (async () => setContacts(await getContacts()))();
  }, []);

  return (
    <div style={{ height: '100vh', width: '100%' }}>
      <DataGrid
        components={{
          Toolbar: GridToolbar,
        }}
        rows={contacts}
        columns={columns}
        pageSize={10}
        rowsPerPageOptions={[5]}
        disableSelectionOnClick
        filterMode='client'
      />
    </div>
  );
}

const columns = [
  { field: 'name', headerName: 'Name' },
  {
    field: 'dateOfBirth',
    headerName: 'Date of birth',
    editable: true,
  },
  {
    field: 'married',
    headerName: 'Married',
    type: 'boolean',
    editable: true,
  },
  {
    field: 'phone',
    headerName: 'Phone',
    editable: true,
  },
  {
    field: 'salary',
    headerName: 'Salary',
    type: 'number',
  },
];
