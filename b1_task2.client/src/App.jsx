import React, { Component } from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import FileList from './FileList.jsx';
import FileData from './FileData.jsx';
import FileUpload from './FileUpload.jsx';

const App = () => {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<FileList />} />
                <Route path="api/Excel/filedata/:fileID" element={<FileData />} />
                <Route path="/upload" element={<FileUpload />} />
            </Routes>
        </Router>
    );
};

export default App;
  