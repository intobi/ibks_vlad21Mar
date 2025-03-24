'use client';

import React from 'react';
import {
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    Paper,
    TablePagination,
    Chip
} from '@mui/material';
import Link from 'next/link';
import { TicketListItemDto } from '@/api-client';

const StatusChip = ({ status }: { status: string | null | undefined }) => {
    let color: 'default' | 'primary' | 'secondary' | 'error' | 'info' | 'success' | 'warning' = 'default';

    if (!status) return <Chip label="Unknown" color="default" size="small" />;

    switch (status.toLowerCase()) {
        case 'open':
            color = 'primary';
            break;
        case 'new':
            color = 'secondary';
            break;
        case 'awaiting response - user':
        case 'awaiting response - development':
        case 'awaiting response - vendor':
            color = 'info';
            break;
        case 'closed':
            color = 'success';
            break;
        default:
            color = 'default';
    }

    return <Chip label={status} color={color} size="small" />;
};

const PriorityChip = ({ priority }: { priority: string | null | undefined }) => {
    let color: 'default' | 'primary' | 'secondary' | 'error' | 'info' | 'success' | 'warning' = 'default';

    if (!priority) return <Chip label="None" color="default" size="small" />;

    switch (priority.toLowerCase()) {
        case 'high':
            color = 'error';
            break;
        case 'medium':
            color = 'warning';
            break;
        case 'low':
            color = 'success';
            break;
        default:
            color = 'default';
    }

    return <Chip label={priority} color={color} size="small" />;
};

type TicketsTableProps = {
    tickets: TicketListItemDto[];
    totalCount: number;
    page: number;
    rowsPerPage: number;
    onPageChange: (event: unknown, newPage: number) => void;
    onRowsPerPageChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
};

export default function TicketsTable({
    tickets,
    totalCount,
    page,
    rowsPerPage,
    onPageChange,
    onRowsPerPageChange
}: TicketsTableProps) {
    return (
        <Paper elevation={3}>
            <TableContainer component={Paper}>
                <Table aria-label="ticket table">
                    <TableHead>
                        <TableRow>
                            <TableCell>ID</TableCell>
                            <TableCell>Title</TableCell>
                            <TableCell>Application</TableCell>
                            <TableCell>Type</TableCell>
                            <TableCell>Priority</TableCell>
                            <TableCell>Status</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {tickets.length > 0 ? (
                            tickets.map((ticket) => (
                                <TableRow
                                    key={ticket.id}
                                    hover
                                    sx={{
                                        cursor: 'pointer',
                                        '&:hover': {
                                            backgroundColor: 'rgba(0, 0, 0, 0.04)',
                                        }
                                    }}
                                    onClick={() => {
                                    }}
                                >
                                    <TableCell>
                                        <Link href={`/tickets/${ticket.id}`} style={{ color: 'inherit', textDecoration: 'none', display: 'block' }}>
                                            {ticket.id}
                                        </Link>
                                    </TableCell>
                                    <TableCell>
                                        <Link href={`/tickets/${ticket.id}`} style={{ color: 'inherit', textDecoration: 'none', display: 'block' }}>
                                            {ticket.title || 'Untitled'}
                                        </Link>
                                    </TableCell>
                                    <TableCell>
                                        <Link href={`/tickets/${ticket.id}`} style={{ color: 'inherit', textDecoration: 'none', display: 'block' }}>
                                            {ticket.applicationName || 'N/A'}
                                        </Link>
                                    </TableCell>
                                    <TableCell>
                                        <Link href={`/tickets/${ticket.id}`} style={{ color: 'inherit', textDecoration: 'none', display: 'block' }}>
                                            {ticket.ticketType?.title || 'N/A'}
                                        </Link>
                                    </TableCell>
                                    <TableCell>
                                        <Link href={`/tickets/${ticket.id}`} style={{ color: 'inherit', textDecoration: 'none', display: 'block' }}>
                                            <PriorityChip priority={ticket.priority?.title} />
                                        </Link>
                                    </TableCell>
                                    <TableCell>
                                        <Link href={`/tickets/${ticket.id}`} style={{ color: 'inherit', textDecoration: 'none', display: 'block' }}>
                                            <StatusChip status={ticket.status?.title} />
                                        </Link>
                                    </TableCell>
                                </TableRow>
                            ))
                        ) : (
                            <TableRow>
                                <TableCell colSpan={6} align="center">
                                    No tickets found
                                </TableCell>
                            </TableRow>
                        )}
                    </TableBody>
                </Table>
            </TableContainer>
            <TablePagination
                rowsPerPageOptions={[5, 10, 25]}
                component="div"
                count={totalCount}
                rowsPerPage={rowsPerPage}
                page={page}
                onPageChange={onPageChange}
                onRowsPerPageChange={onRowsPerPageChange}
            />
        </Paper>
    );
}