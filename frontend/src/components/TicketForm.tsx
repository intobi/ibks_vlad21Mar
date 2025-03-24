'use client';

import React, { useState, useEffect } from 'react';
import {
    Box,
    TextField,
    Button,
    FormControl,
    InputLabel,
    Select,
    MenuItem,
    Typography,
    Grid,
    Paper,
    CircularProgress,
    SelectChangeEvent,
    Divider
} from '@mui/material';
import {
    Configuration,
    ReferenceDataApi,
    TicketRequestDto,
    PriorityDto,
    StatusDto,
    TicketTypeDto,
    TicketDto,
    UserDto,
    InstalledEnvironmentDto
} from '@/api-client';

interface TicketFormProps {
    initialData?: TicketDto;
    onSubmit: (data: TicketRequestDto) => Promise<void>;
    submitButtonText?: string;
    isLoading?: boolean;
}

export default function TicketForm({
    initialData = {},
    onSubmit,
    submitButtonText = 'Submit',
    isLoading = false
}: TicketFormProps) {
    const [formData, setFormData] = useState<TicketRequestDto>({
        title: initialData.title,
        description: initialData.description,
        applicationName: initialData.applicationName,
        stackTrace: initialData.stackTrace,
        device: initialData.device,
        browser: initialData.browser,
        resolution: initialData.resolution,
        url: initialData.url,
        priorityId: initialData.priority?.id,
        ticketTypeId: initialData.ticketType?.id,
        statusId: initialData.status?.id,
        userOID: initialData.user?.oid,
        installedEnvironmentId: initialData.installedEnvironment?.id
    });
    const [priorities, setPriorities] = useState<PriorityDto[]>([]);
    const [statuses, setStatuses] = useState<StatusDto[]>([]);
    const [ticketTypes, setTicketTypes] = useState<TicketTypeDto[]>([]);
    const [users, setUsers] = useState<UserDto[]>([]);
    const [installedEnvironments, setInstalledEnvironments] = useState<InstalledEnvironmentDto[]>([]);
    const [isReferenceDataLoading, setIsReferenceDataLoading] = useState(true);
    const [referenceDataError, setReferenceDataError] = useState<string | null>(null);

    const apiClient = new ReferenceDataApi(new Configuration({
        basePath: process.env.NEXT_PUBLIC_API_URL,
    }));

    useEffect(() => {
        const fetchReferenceData = async () => {
            setIsReferenceDataLoading(true);
            try {
                const [prioritiesData, statusesData, typesData, usersData, environemtnsData] = await Promise.all([
                    apiClient.apiReferenceDataPrioritiesGet(),
                    apiClient.apiReferenceDataStatusesGet(),
                    apiClient.apiReferenceDataTypesGet(),
                    apiClient.apiReferenceDataUsersGet(),
                    apiClient.apiReferenceDataInstalledEnvironmentsGet()
                ]);

                setPriorities(prioritiesData);
                setStatuses(statusesData);
                setTicketTypes(typesData);
                setUsers(usersData);
                setInstalledEnvironments(environemtnsData);
            } catch (err) {
                console.error('Failed to load reference data:', err);
                setReferenceDataError('Failed to load form data. Please try again later.');
            } finally {
                setIsReferenceDataLoading(false);
            }
        };

        fetchReferenceData();
    }, []);

    const handleTextChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setFormData(prev => ({
            ...prev,
            [name]: value
        }));
    };

    const handleSelectChange = (e: SelectChangeEvent<number | string>) => {
        const { name, value } = e.target;
        setFormData(prev => ({
            ...prev,
            [name]: value
        }));
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        await onSubmit(formData);
    };

    if (isReferenceDataLoading) {
        return (
            <Box display="flex" justifyContent="center" alignItems="center" minHeight="400px" >
                <CircularProgress />
            </Box>
        );
    }

    if (referenceDataError) {
        return (
            <Box display="flex" justifyContent="center" alignItems="center" minHeight="400px" >
                <Typography color="error" > {referenceDataError} </Typography>
            </Box>
        );
    }

    return (
        <Paper elevation={3} sx={{ p: 3 }
        }>
            <form onSubmit={handleSubmit}>
                <Grid container spacing={3} >
                    {/* Basic Information Section */}
                    <Grid item xs={12}>
                        <Typography variant="h6" gutterBottom>
                            Basic Information
                        </Typography>
                        <Divider sx={{ mb: 2 }} />
                    </Grid>

                    {/* Title Field */}
                    < Grid item xs={12} >
                        <TextField
                            fullWidth
                            required
                            label="Title"
                            name="title"
                            value={formData.title || ''}
                            onChange={handleTextChange}
                            variant="outlined"
                        />
                    </Grid>

                    {/* Description Field */}
                    <Grid item xs={12} >
                        <TextField
                            fullWidth
                            required
                            label="Description"
                            name="description"
                            value={formData.description || ''}
                            onChange={handleTextChange}
                            variant="outlined"
                            multiline
                            rows={4}
                        />
                    </Grid>

                    {/* Application Name Field */}
                    <Grid item xs={12} md={6} >
                        <TextField
                            fullWidth
                            label="Application Name"
                            name="applicationName"
                            value={formData.applicationName || ''}
                            onChange={handleTextChange}
                            variant="outlined"
                        />
                    </Grid>

                    {/* Installed Environment Dropdown */}
                    <Grid item xs={12} md={6}>
                        <FormControl fullWidth>
                            <InputLabel id="installedEnvironmentId-label">Environment</InputLabel>
                            <Select
                                labelId="installedEnvironmentId-label"
                                id="installedEnvironmentId"
                                name="installedEnvironmentId"
                                value={formData.installedEnvironmentId || ''}
                                label="Environment"
                                onChange={handleSelectChange}
                            >
                                {installedEnvironments.map((env) => (
                                    <MenuItem key={env.id} value={env.id}>
                                        {env.title}
                                    </MenuItem>
                                ))}
                            </Select>
                        </FormControl>
                    </Grid>

                    {/* Technical Details Section */}
                    <Grid item xs={12} sx={{ mt: 2 }}>
                        <Typography variant="h6" gutterBottom>
                            Technical Details
                        </Typography>
                        <Divider sx={{ mb: 2 }} />
                    </Grid>

                    {/* Device Field */}
                    <Grid item xs={12} md={4}>
                        <TextField
                            fullWidth
                            label="Device"
                            name="device"
                            value={formData.device || ''}
                            onChange={handleTextChange}
                            variant="outlined"
                        />
                    </Grid>

                    {/* Browser Field */}
                    <Grid item xs={12} md={4}>
                        <TextField
                            fullWidth
                            label="Browser"
                            name="browser"
                            value={formData.browser || ''}
                            onChange={handleTextChange}
                            variant="outlined"
                        />
                    </Grid>

                    {/* Resolution Field */}
                    <Grid item xs={12} md={4}>
                        <TextField
                            fullWidth
                            label="Resolution"
                            name="resolution"
                            value={formData.resolution || ''}
                            onChange={handleTextChange}
                            variant="outlined"
                        />
                    </Grid>

                    {/* URL field */}
                    <Grid item xs={12}>
                        <TextField
                            fullWidth
                            label="URL"
                            name="url"
                            value={formData.url || ''}
                            onChange={handleTextChange}
                            variant="outlined"
                        />
                    </Grid>

                    {/* Stack Trace Field */}
                    <Grid item xs={12}>
                        <TextField
                            fullWidth
                            label="Stack Trace"
                            name="stackTrace"
                            value={formData.stackTrace || ''}
                            onChange={handleTextChange}
                            variant="outlined"
                            multiline
                            rows={4}
                        />
                    </Grid>

                    {/* Status & Classification Section */}
                    <Grid item xs={12} sx={{ mt: 2 }}>
                        <Typography variant="h6" gutterBottom>
                            Status & Classification
                        </Typography>
                        <Divider sx={{ mb: 2 }} />
                    </Grid>

                    {/* Ticket Type Dropdown */}
                    <Grid item xs={12} md={6} >
                        <FormControl fullWidth required >
                            <InputLabel id="ticketTypeId-label" > Ticket Type </InputLabel>
                            < Select
                                labelId="ticketTypeId-label"
                                id="ticketTypeId"
                                name="ticketTypeId"
                                value={formData.ticketTypeId || ''}
                                label="Ticket Type"
                                onChange={handleSelectChange}
                            >
                                {
                                    ticketTypes.map((type) => (
                                        <MenuItem key={type.id} value={type.id} >
                                            {type.title}
                                        </MenuItem>
                                    ))
                                }
                            </Select>
                        </FormControl>
                    </Grid>

                    {/* Priority Dropdown */}
                    <Grid item xs={12} md={6} >
                        <FormControl fullWidth required >
                            <InputLabel id="priorityId-label" > Priority </InputLabel>
                            < Select
                                labelId="priorityId-label"
                                id="priorityId"
                                name="priorityId"
                                value={formData.priorityId || ''}
                                label="Priority"
                                onChange={handleSelectChange}
                            >
                                {
                                    priorities.map((priority) => (
                                        <MenuItem key={priority.id} value={priority.id} >
                                            {priority.title}
                                        </MenuItem>
                                    ))
                                }
                            </Select>
                        </FormControl>
                    </Grid>

                    {/* Status Dropdown */}
                    <Grid item xs={12} md={6} >
                        <FormControl fullWidth required >
                            <InputLabel id="statusId-label" > Status </InputLabel>
                            < Select
                                labelId="statusId-label"
                                id="statusId"
                                name="statusId"
                                value={formData.statusId || ''}
                                label="Status"
                                onChange={handleSelectChange}
                            >
                                {
                                    statuses.map((status) => (
                                        <MenuItem key={status.id} value={status.id} >
                                            {status.title}
                                        </MenuItem>
                                    ))
                                }
                            </Select>
                        </FormControl>
                    </Grid>

                    {/* User Dropdown */}
                    <Grid item xs={12} md={6}>
                        <FormControl fullWidth>
                            <InputLabel id="userOID-label">User</InputLabel>
                            <Select
                                labelId="userOID-label"
                                id="userOID"
                                name="userOID"
                                value={formData.userOID || ''}
                                label="User"
                                onChange={handleSelectChange}
                            >
                                {users.map((user) => (
                                    <MenuItem key={user.oid} value={user.oid}>
                                        {user.displayName || user.fullName || user.email}
                                    </MenuItem>
                                ))}
                            </Select>
                        </FormControl>
                    </Grid>

                    {/* Submit Button */}
                    <Grid item xs={12} >
                        <Box display="flex" justifyContent="flex-end" >
                            <Button
                                type="submit"
                                variant="contained"
                                color="primary"
                                disabled={isLoading}
                                sx={{ minWidth: 120 }}
                            >
                                {isLoading ? <CircularProgress size={24} /> : submitButtonText}
                            </Button>
                        </Box>
                    </Grid>
                </Grid>
            </form>
        </Paper>
    );
}