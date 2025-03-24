// app/tickets/create/page.tsx
'use client';

import React, { useState } from 'react';
import { Configuration, TicketsApi, TicketRequestDto } from '@/api-client';
import TicketForm from '@/components/TicketForm';
import { Box, Typography, Alert, Button } from '@mui/material';
import { useRouter } from 'next/navigation';
import { ArrowBack } from '@mui/icons-material';

export default function CreateTicketPage() {
    const [isSubmitting, setIsSubmitting] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const router = useRouter();

    const apiClient = new TicketsApi(new Configuration({
        basePath: process.env.NEXT_PUBLIC_API_URL
    }));

    const handleSubmit = async (data: TicketRequestDto) => {
        setIsSubmitting(true);
        setError(null);

        try {
            const response = await apiClient.apiTicketsPost({
                ticketRequestDto: data
            });

            router.push(`/tickets/${response.id}`);
        } catch (err) {
            console.error('Failed to create ticket:', err);
            setError('Failed to create ticket. Please try again.');
            setIsSubmitting(false);
        }
    };

    return (
        <Box sx={{ py: 4 }}>
            <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 3 }}>
                <Typography variant="h4" component="h1">
                    Create New Ticket
                </Typography>
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

            <TicketForm
                onSubmit={handleSubmit}
                submitButtonText="Create Ticket"
                isLoading={isSubmitting}
            />
        </Box>
    );
}