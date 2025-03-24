'use client';

import React, { useState, useEffect } from 'react';
import { Configuration, TicketsApi, TicketRequestDto, TicketDto } from '@/api-client';
import TicketForm from '@/components/TicketForm';
import { Box, Typography, Alert, CircularProgress, Tabs, Tab, Divider, Button } from '@mui/material';
import { useRouter } from 'next/navigation';
import { useParams } from 'next/navigation';
import ReplySection from '@/components/ReplySection';
import { ArrowBack } from '@mui/icons-material';

interface TabPanelProps {
    children?: React.ReactNode;
    index: number;
    value: number;
}

function TabPanel({ children, value, index, ...other }: TabPanelProps) {
    return (
        <div
            role="tabpanel"
            hidden={value !== index}
            id={`ticket-tabpanel-${index}`}
            aria-labelledby={`ticket-tab-${index}`}
            {...other}
        >
            {value === index && (
                <Box sx={{ py: 3 }}>
                    {children}
                </Box>
            )}
        </div>
    );
}

export default function EditTicketPage() {
    const [ticket, setTicket] = useState<TicketDto | null>(null);
    const [isLoading, setIsLoading] = useState(true);
    const [isSubmitting, setIsSubmitting] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const [tabValue, setTabValue] = useState(0);
    const router = useRouter();
    const params = useParams();
    const ticketId = Number(params.id);

    const apiClient = new TicketsApi(new Configuration({
        basePath: process.env.NEXT_PUBLIC_API_URL,
    }));

    useEffect(() => {
        const fetchTicket = async () => {
            setIsLoading(true);
            try {
                const response = await apiClient.apiTicketsIdGet({
                    id: ticketId
                });
                setTicket(response);
            } catch (err) {
                console.error('Failed to fetch ticket:', err);
                setError('Failed to load ticket data. Please try again later.');
            } finally {
                setIsLoading(false);
            }
        };

        if (ticketId) {
            fetchTicket();
        }
    }, [ticketId]);

    const handleSubmit = async (data: TicketRequestDto) => {
        setIsSubmitting(true);
        setError(null);

        try {
            await apiClient.apiTicketsIdPut({
                id: ticketId,
                ticketRequestDto: data
            });

            router.refresh();

            const updatedTicket = await apiClient.apiTicketsIdGet({
                id: ticketId
            });

            setTicket(updatedTicket);
            setTabValue(0);
        } catch (err) {
            console.error('Failed to update ticket:', err);
            setError('Failed to update ticket. Please try again.');
        } finally {
            setIsSubmitting(false);
        }
    };

    const handleTabChange = (_event: React.SyntheticEvent, newValue: number) => {
        setTabValue(newValue);
    };

    console.log(Intl.DateTimeFormat().resolvedOptions().timeZone);

    if (isLoading) {
        return (
            <Box display="flex" justifyContent="center" alignItems="center" minHeight="400px">
                <CircularProgress />
            </Box>
        );
    }

    if (error && !ticket) {
        return (
            <Box display="flex" justifyContent="center" alignItems="center" minHeight="400px">
                <Alert severity="error">{error}</Alert>
            </Box>
        );
    }

    return (
        <Box sx={{ py: 4 }}>
            <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 3 }}>
                <Box>
                    <Typography variant="h4" component="h1" gutterBottom>
                        {ticket?.title || 'Ticket Details'}
                    </Typography>
                    <Box sx={{ display: 'flex', gap: 3, color: 'text.secondary', mb: 1 }}>
                        {ticket?.date && (
                            <Typography variant="body2">
                                <strong>Created:</strong> {new Date(ticket.date).toLocaleString()}
                            </Typography>
                        )}
                        {ticket?.lastModified && (
                            <Typography variant="body2">
                                <strong>Last Updated:</strong> {new Date(ticket.lastModified).toLocaleString()}
                            </Typography>
                        )}
                    </Box>
                </Box>
                <Button
                    variant="outlined"
                    startIcon={<ArrowBack />}
                    onClick={() => router.push('/tickets')}
                >
                    Close
                </Button>
            </Box>

            {error && (
                <Alert severity="error" sx={{ mb: 3 }}>
                    {error}
                </Alert>
            )}

            <Box sx={{ borderBottom: 1, borderColor: 'divider', mb: 3 }}>
                <Tabs value={tabValue} onChange={handleTabChange} aria-label="ticket tabs">
                    <Tab label="Details" id="ticket-tab-0" aria-controls="ticket-tabpanel-0" />
                    <Tab label="Replies" id="ticket-tab-1" aria-controls="ticket-tabpanel-1" />
                </Tabs>
            </Box>

            <TabPanel value={tabValue} index={0}>
                <TicketForm
                    initialData={ticket ?? {}}
                    onSubmit={handleSubmit}
                    submitButtonText="Update Ticket"
                    isLoading={isSubmitting}
                />
            </TabPanel>

            <TabPanel value={tabValue} index={1}>
                {ticket && <ReplySection ticketId={ticketId} />}
            </TabPanel>
        </Box>
    );
}