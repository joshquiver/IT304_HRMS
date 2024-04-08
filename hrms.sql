-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 08, 2024 at 09:08 AM
-- Server version: 10.4.28-MariaDB
-- PHP Version: 8.0.28

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `hrms`
--

-- --------------------------------------------------------

--
-- Table structure for table `attendance`
--

CREATE TABLE `attendance` (
  `id` int(11) NOT NULL,
  `Employee_ID` varchar(50) NOT NULL,
  `Time_In_Time_Out` varchar(255) DEFAULT NULL,
  `AM_PM` varchar(50) NOT NULL,
  `Time` varchar(255) DEFAULT NULL,
  `Date` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `attendance`
--

INSERT INTO `attendance` (`id`, `Employee_ID`, `Time_In_Time_Out`, `AM_PM`, `Time`, `Date`) VALUES
(1, '342', '[value-3]', '', '[value-4]', '[value-5]'),
(2, '342', 'TIME IN', '', ' 12:00:00 am', '12-03-2023'),
(3, '342', 'TIME IN', '', ' 06:58 pm', '12-03-2023'),
(4, '4534', 'TIME IN', 'AM', ' 08:11 pm', '12-03-2023'),
(5, '342', '', '', ' 08:13 pm', '12-03-2023'),
(6, '342', '', '', ' 08:14 pm', '12-03-2023'),
(7, '4534', 'TIME IN', 'PM', '02:03 pm', '04-04-2024');

-- --------------------------------------------------------

--
-- Table structure for table `employees`
--

CREATE TABLE `employees` (
  `id` int(50) NOT NULL,
  `Employee_ID` varchar(50) DEFAULT NULL,
  `Full_Name` varchar(100) DEFAULT NULL,
  `Gender` varchar(10) DEFAULT NULL,
  `Contact_Number` varchar(20) DEFAULT NULL,
  `Position` varchar(50) DEFAULT NULL,
  `Image` varchar(255) DEFAULT NULL,
  `Salary` int(11) NOT NULL,
  `Insert_Date` varchar(50) DEFAULT NULL,
  `Update_Date` varchar(50) DEFAULT NULL,
  `Delete_Date` varchar(50) DEFAULT NULL,
  `Status` varchar(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `employees`
--

INSERT INTO `employees` (`id`, `Employee_ID`, `Full_Name`, `Gender`, `Contact_Number`, `Position`, `Image`, `Salary`, `Insert_Date`, `Update_Date`, `Delete_Date`, `Status`) VALUES
(15, '4534', 'hghjhj', 'Male', '676', 'Instructor 1', 'C:UsersAngeloDesktopHumanResourcesManagementSystemHumanResourcesManagementSystemDirectory4534.jpg', 1000, '0', '04-04-2024', NULL, 'Active'),
(16, '3423', 'fgdfg', 'Male', '454', 'Business Management', 'C:UsersAngeloDesktopHumanResourcesManagementSystemHumanResourcesManagementSystemDirectory3423.jpg', 3001, '0', '12-03-2023', '04-04-2024', 'Active'),
(17, '5345', 'gtthtfhh', 'Male', '5656', 'Business Management', 'C:UsersAngeloDesktopHumanResourcesManagementSystemHumanResourcesManagementSystemDirectory5345.jpg', 12, '0', NULL, NULL, 'Active'),
(18, '342', 'jomboy', 'Male', '454', 'Business Management', 'C:UsersAngeloDesktopHumanResourcesManagementSystemHumanResourcesManagementSystemDirectory342.jpg', 0, '12-03-2023', '12-03-2023', '12-03-2023', 'Active'),
(19, '34231', 'sdffgdfgf', 'Male', '43335', 'Instructor 1', 'C:UsersAngeloDesktopHumanResourcesManagementSystemHumanResourcesManagementSystemDirectory34231.jpg', 0, '12-03-2023', '12-03-2023', '04-04-2024', 'Inactive');

-- --------------------------------------------------------

--
-- Table structure for table `log`
--

CREATE TABLE `log` (
  `log_id` int(11) NOT NULL,
  `id` int(10) NOT NULL,
  `log_message` text NOT NULL,
  `log_time` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `log_failures` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `log`
--

INSERT INTO `log` (`log_id`, `id`, `log_message`, `log_time`, `log_failures`) VALUES
(1, 0, 'Successful login for user: admin', '2024-04-04 04:05:48', '0'),
(2, -1, 'Successful login for user: admin', '2024-04-04 04:32:19', '0'),
(3, -1, 'Failed login attempt for user: admin', '2024-04-04 04:34:23', '1'),
(4, 0, 'Failed login attempt for user: admin', '2024-04-04 04:44:27', '1'),
(5, 0, 'Failed login attempt for user: admin', '2024-04-04 04:45:49', '1'),
(6, 0, 'Failed login attempt for user: admin', '2024-04-04 04:52:52', '1'),
(7, 0, 'Failed login attempt for user: admin', '2024-04-04 05:25:31', '1'),
(8, 0, 'Failed login attempt for user: admin', '2024-04-04 05:45:42', '1'),
(9, 0, 'Successful login for user: admin', '2024-04-04 05:49:59', '0'),
(10, 0, 'Successful login for user: admin', '2024-04-04 05:54:12', '0'),
(11, 0, 'Successful login for user: admin', '2024-04-04 05:55:26', '0'),
(12, 0, 'Successful login for user: admin', '2024-04-04 05:56:32', '0'),
(13, 1, 'Successfuly logout for user: Welcome, Admin', '2024-04-04 05:56:37', '0'),
(14, 0, 'Successful login for user: admin', '2024-04-04 06:02:41', '0'),
(15, 4534, 'Successfully saved for user: 4534', '2024-04-04 06:03:10', '0'),
(16, 0, 'Successful login for user: admin', '2024-04-04 06:05:07', '0'),
(17, 0, 'Successful login for user: admin', '2024-04-04 06:06:38', '0'),
(18, 0, 'Failed login attempt for user: admin', '2024-04-04 06:09:17', '1'),
(19, 0, 'Successful login for user: admin', '2024-04-04 06:09:23', '0'),
(20, 0, 'Successful login for user: admin', '2024-04-04 06:13:42', '0'),
(21, 4534, 'Successfully UPDATE SALARY  In for userID: 4534', '2024-04-04 06:14:00', '0'),
(22, 0, 'Successful login for user: admin', '2024-04-04 06:15:57', '0'),
(23, 0, 'Successful login for user: admin', '2024-04-04 06:18:04', '0'),
(24, 34231, 'Successfully DELETED EMPLOYEE In for user: 34231', '2024-04-04 06:18:25', '0'),
(25, 1, 'Successfully logout for user: Admin', '2024-04-04 06:18:53', '0'),
(26, 0, 'Failed login attempt for user: admin', '2024-04-04 06:19:02', '1'),
(27, 0, 'Failed login attempt for user: admin', '2024-04-04 06:19:57', '1'),
(28, 0, 'Successful login for user: admin', '2024-04-04 06:20:22', '0'),
(29, 3423, 'Successfully DELETED EMPLOYEE In for user: 3423', '2024-04-04 06:21:27', '0'),
(30, 4534, 'Successfully UPDATE SALARY  In for userID: 4534', '2024-04-04 06:22:00', '0'),
(31, 1, 'Successfully logout for user: Admin', '2024-04-04 06:22:28', '0');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `Username` varchar(255) NOT NULL,
  `Password` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`id`, `Username`, `Password`) VALUES
(1, 'admin', 'admin123');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `attendance`
--
ALTER TABLE `attendance`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `employees`
--
ALTER TABLE `employees`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `log`
--
ALTER TABLE `log`
  ADD PRIMARY KEY (`log_id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `attendance`
--
ALTER TABLE `attendance`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `employees`
--
ALTER TABLE `employees`
  MODIFY `id` int(50) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- AUTO_INCREMENT for table `log`
--
ALTER TABLE `log`
  MODIFY `log_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=32;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
