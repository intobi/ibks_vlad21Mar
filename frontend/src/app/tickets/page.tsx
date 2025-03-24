'use client';

import { Configuration, TicketsApi, TicketListItemDto } from '@/api-client';
import React, { useState, useEffect } from 'react';
import {
    CircularProgress,
    Box,
    Typography,
    Link,
    Button,
} from '@mui/material';
import AddIcon from '@mui/icons-material/Add';
import TicketsTable from '@/components/TicketsTable';

export default function TicketsTablePage() {
    const [tickets, setTickets] = useState<TicketListItemDto[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);
    const [totalCount, setTotalCount] = useState<number>(0);

    const [page, setPage] = useState<number>(0);
    const [rowsPerPage, setRowsPerPage] = useState<number>(10);

    const apiClient = new TicketsApi(new Configuration({
        basePath: process.env.NEXT_PUBLIC_API_URL,
    }));

    const fetchTickets = async () => {
        setLoading(true);
        try {
            const response = await apiClient.apiTicketsGet({
                page: page + 1,
                pageSize: rowsPerPage,
            });

            setTickets(response.items || []);
            setTotalCount(response.totalCount || 0);
        } catch (err) {
            console.error('Failed to fetch tickets:', err);
            setError('Failed to load tickets. Please try again later.');
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchTickets();
    }, [page, rowsPerPage]);

    const handleChangePage = (_event: unknown, newPage: number) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPageChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(0);
    };

    if (loading && tickets.length === 0) {
        return (
            <Box display="flex" justifyContent="center" alignItems="center" minHeight="400px">
                <CircularProgress />
            </Box>
        );
    }

    if (error && tickets.length === 0) {
        return (
            <Box display="flex" justifyContent="center" alignItems="center" minHeight="400px">
                <Typography color="error">{error}</Typography>
            </Box>
        );
    }

    return (
        <Box sx={{ py: 4 }}>
            <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 3 }}>
                <Typography variant="h4" component="h1">
                    Tickets
                </Typography>
                <Link href="/tickets/create" style={{ textDecoration: 'none' }}>
                    <Button
                        variant="contained"
                        color="primary"
                        startIcon={<AddIcon />}
                    >
                        Add New Ticket
                    </Button>
                </Link>
            </Box>
            <TicketsTable
                tickets={tickets}
                totalCount={totalCount}
                page={page}
                rowsPerPage={rowsPerPage}
                onPageChange={handleChangePage}
                onRowsPerPageChange={handleChangeRowsPerPageChange}
            />
        </Box>
    );
}