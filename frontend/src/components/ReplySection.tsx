'use client';

import React, { useState, useEffect } from 'react';
import {
    Box,
    Typography,
    TextField,
    Button,
    Paper,
    Divider,
    List,
    ListItem,
    Avatar,
    CircularProgress,
    Alert
} from '@mui/material';
import { Configuration, RepliesApi, TicketReplyDto } from '@/api-client';

interface ReplySectionProps {
    ticketId: number;
}

export default function ReplySection({ ticketId }: ReplySectionProps) {
    const [replies, setReplies] = useState<TicketReplyDto[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);
    const [newReply, setNewReply] = useState('');
    const [isSubmitting, setIsSubmitting] = useState(false);

    const apiClient = new RepliesApi(new Configuration({
        basePath: process.env.NEXT_PUBLIC_API_URL,
    }));

    useEffect(() => {
        const fetchReplies = async () => {
            setIsLoading(true);
            try {
                const response = await apiClient.apiRepliesTicketTicketIdGet({
                    ticketId: ticketId
                });

                setReplies(response || []);
            } catch (err) {
                console.error('Failed to fetch replies:', err);
                setError('Failed to load replies. Please try again later.');
            } finally {
                setIsLoading(false);
            }
        };

        fetchReplies();
    }, [ticketId]);

    const handleSubmitReply = async () => {
        if (!newReply.trim()) return;

        setIsSubmitting(true);
        try {
            await apiClient.apiRepliesPost({
                ticketReplyDto: {
                    tId: ticketId,
                    reply: newReply
                }
            });

            const response = await apiClient.apiRepliesTicketTicketIdGet({
                ticketId: ticketId
            });

            setReplies(response || []);
            setNewReply('');
        } catch (err) {
            console.error('Failed to post reply:', err);
            setError('Failed to post reply. Please try again.');
        } finally {
            setIsSubmitting(false);
        }
    };

    if (isLoading && replies.length === 0) {
        return (
            <Box display="flex" justifyContent="center" alignItems="center" minHeight="200px">
                <CircularProgress />
            </Box>
        );
    }

    return (
        <Box>
            <Typography variant="h6" gutterBottom>
                Conversation
            </Typography>

            {error && (
                <Alert severity="error" sx={{ mb: 3 }}>
                    {error}
                </Alert>
            )}

            <Paper variant="outlined" sx={{ mb: 4, p: 0 }}>
                <List sx={{ p: 0 }}>
                    {replies.length > 0 ? (
                        replies.map((reply, index) => (
                            <React.Fragment key={reply.replyId || index}>
                                <ListItem sx={{ py: 2, px: 3, display: 'block' }}>
                                    <Box sx={{ display: 'flex', alignItems: 'center', mb: 1 }}>
                                        <Typography variant="caption" color="text.secondary">
                                            {reply.replyDate?.toLocaleString()}
                                        </Typography>
                                    </Box>
                                    <Typography variant="body1">
                                        {reply.reply}
                                    </Typography>
                                </ListItem>
                                {index < replies.length - 1 && <Divider />}
                            </React.Fragment>
                        ))
                    ) : (
                        <ListItem>
                            <Typography color="text.secondary">
                                No replies yet. Be the first to respond!
                            </Typography>
                        </ListItem>
                    )}
                </List>
            </Paper>

            <Typography variant="h6" gutterBottom>
                Add Reply
            </Typography>

            <Paper elevation={0} variant="outlined" sx={{ p: 3 }}>
                <TextField
                    fullWidth
                    multiline
                    rows={4}
                    placeholder="Type your reply here..."
                    value={newReply}
                    onChange={(e) => setNewReply(e.target.value)}
                    variant="outlined"
                    sx={{ mb: 2 }}
                />
                <Box display="flex" justifyContent="flex-end">
                    <Button
                        variant="contained"
                        color="primary"
                        onClick={handleSubmitReply}
                        disabled={isSubmitting || !newReply.trim()}
                    >
                        {isSubmitting ? <CircularProgress size={24} /> : 'Post Reply'}
                    </Button>
                </Box>
            </Paper>
        </Box>
    );
}