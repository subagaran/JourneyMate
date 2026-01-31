using PickPayEnterpriseSolutionLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace PickPayEnterprise
{
    public partial class FormViewInvoice : Form
    {
        private int gBillId = 0;
        public string TABLENAME = "tblReceiveItemBr";
        public string PaymentType = "CASH";
        public FormViewInvoice()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            FormViewBills viewBills = new FormViewBills();
            viewBills.TABLENAME = TABLENAME;
            viewBills.ShowDialog();
            if (viewBills.gOkCancel)
            {
                txtRefNo.Text = viewBills.gRefNo;
                gBillId = viewBills.gBillId;
                cboCashorCard.Text = viewBills.gDescription;
                txtRefNo.Focus();
            }
        }

        private void FormViewInvoice_Load(object sender, EventArgs e)
        {

        }





        private void txtRefNo_Validating(object sender, CancelEventArgs e)
        {
            string Query = "";

            if (string.IsNullOrEmpty(txtRefNo.Text))
            {
                return;
            }

            if (TABLENAME.ToUpper() == "TBLINVOICEBR")
            {
                 Query = "Select TranId from " + TABLENAME + " where BillNo = '" + txtRefNo.Text + "' AND CompanyId = " + CommonVariables.GCompanyId + "" + GetActivQuery() + GetBillTypeBasedOn();
                //Query = "Select TranId from " + TABLENAME + " where  TranId = " + gBillId + " AND CompanyId = " + CommonVariables.GCompanyId + "" + GetActivQuery() + GetBillTypeBasedOn();

            }
            else if (TABLENAME.ToUpper() == "TBLINVOICEBRVAT")
            {
                Query = "Select TranId from TBLINVOICEBR where BillNo = '" + txtRefNo.Text + "' AND CompanyId = " + CommonVariables.GCompanyId + "" + GetActivQuery() + GetBillTypeBasedOn();
            }
            else
            {
                Query = "Select TranId from " + TABLENAME + " where Refno = '" + txtRefNo.Text + "' AND CompanyId = " + CommonVariables.GCompanyId + "" + GetActivQuery();

            }
            if (GlobalData.RecordExists(Query))
            {
                object result = GlobalData.GetSingleValue(Query, null);
                if (result != null)
                {
                    gBillId = Convert.ToInt32(result);
                    LoadReport();
                }
            }
            else
            {
                MessageBoxHelper.ShowWarning("Ref no not exist");
            }
        }

        private string GetBillTypeBasedOn()
        {
            //switch (GlobalData.GUserRole.ToUpper())
            //{
            //    case "ADMIN":
            //        return " ";
            //    case "USER":
            //        return "AND Description = 'CARD'";
            //    case "MANAGER":
            //        return " AND Description = 'CARD'";
            //    default:
            //        return " ";
            //}

            return " AND Description = '" + cboCashorCard.Text + "' ";
        }
        private void LoadReport()
        {
            string tmpQuery = "";
            string tmpheader = "";
            string tmpReportName = "";

            tmpQuery = GetQuery();
            tmpheader = GetHeaderName();
            tmpReportName = GetReportName();


            ReportPrinter.LoadInvoiceReport(tmpQuery, tmpReportName, tmpheader, "", cryRpt);
        }
        private string GetHeaderName()
        {
            string Header = "";
            switch (TABLENAME.ToUpper())
            {
                case "TBLRECEIVEITEMBR":
                    return "Item Receive";
                default:
                    break;
            }
            return Header;
        }
        private string GetReportName()
        {
            string ReportName = "";
             
            return CommonVariables.gReportLocation + "\\ItemReceive.rpt"; 
        }

        private string GetInvoiceRpt()
        {
            int tmpCount = 0;
            string tmpStrCunt = "";
            string tmpReportName = "";
            string strQuery = "";
            strQuery = "select count(*) from tblInvoiceDe where BriefId =" + gBillId + "";

            object result = GlobalData.GetSingleValue(strQuery, null);

            if (result != null)
            {
                tmpStrCunt = result.ToString();
                int.TryParse(tmpStrCunt, out tmpCount);
            }
            else
            {
                tmpCount = 0;
            }

            //if (tmpCount == 1)
            //{
            //    tmpReportName = "\\Invoice1.rpt"; // INVOICE FOR 1 ITEM
            //}
            //else if (tmpCount == 2)
            //{
            //    tmpReportName = "\\Invoice.rpt"; // SMALL INVOICE
            //}
            //else if (tmpCount > 2 && tmpCount <= 3)
            //{
            //    tmpReportName = "\\InvoiceM.rpt"; // MEDIUM INVOICE
            //}
            //else
            //{
            //    tmpReportName = "\\InvoiceB.rpt"; // LARGE INVOICE
            //}

            tmpReportName = "\\InvoiceF.rpt";
            return tmpReportName;
        }

        private string GetActivQuery()
        {
            switch (TABLENAME.ToUpper())
            {
                case "TBLRECEIVEITEMBR":
                    return "AND IsActive = 'Y'";
                case "TBLINVOICEBR":
                    return "AND IsGood = 'Y'";
                default:
                    return "AND IsGood = 'Y'";
            }
            return "";
        }

        private string GetQuery()
        {
            string strQuery = "";
           
            return GetSalesQuery();
        }

        private string GetItemReceiveQuery()
        {
            string strQuery = "";

            strQuery = "SELECT RB.Date AS RBDate, RB.RefNo AS RBRefNo," +
                " RB.GrossTotal AS RBGrossTotal, RB.UserName AS RBUserName, IT.ItemCode AS ITItemCode, IT.Name AS ITName," +
                " IT.Category AS ITCategory, RD.Qty AS RDQty, RD.PriceUSD AS RDPriceUSD," +
                " RD.PriceLKR AS RDPriceLKR, RD.SellingUsd AS RDSellingUsd, RD.SellingLKR AS RDSellingLKR, RD.LineAmount AS RDLineAmount " +
                " FROM tblReceiveItemBr AS RB INNER JOIN " +
                " tblReceiveItemDe AS RD ON RB.TranId = RD.BriefId INNER JOIN " +
                " tblItem AS IT ON RD.ItemId = IT.ItemID " +
                " where RB.TranId = " + gBillId + "";

            return strQuery;
        }
        private string GetSalesQuery()
        {
            string strQuery = "";

            //            strQuery = "SELECT RB.DateOn AS RBDateOn, RB.RefNo AS RBRefNo, RB.BillNo AS RBBillNo, " +
            //"RB.TranCode AS RBTranCode, RB.GrossTotal AS RBGrossTotal, RB.DisAmount AS RBDisAmount, " +
            //"RB.DisRate AS RBDisRate, RB.GrossTotalVat AS RBNetAmount, RB.PaidAmount AS RBPaidAmount, " +
            //"RB.CNAmount AS RBCNAmount, RB.VatAmount AS RBBalance, RB.IsActive AS RBIsActive, RB.CompanyId AS RBCompanyId, " +
            //"RB.UserId AS RBUserId, RB.ComputerName AS RBComputerName, RB.CreatedOn AS RBCreatedOn, " +
            //"RB.PaymentDes AS RBDescription, RB.UserName AS RBUserName, RB.CompName AS RBCompName, RB.IsGood AS RBIsGood, " +
            //"RB.SalesType AS RBSalesType, RB.SalesManId AS RBSalesManId, RB.GrossTotalLKR AS RBGrossTotalLKR, RB.Status AS RBStatus, " +
            //"RD.BriefId AS RDBriefId, RD.ItemId AS RDItemId, RD.Qty AS RDQty, RD.StkWas AS RDStkWas, " +
            //"RD.LineAmount AS RDLineAmount, RD.PriceUSD AS RDPriceUSD, RD.PriceLKR AS RDPriceLKR, RD.SellingUsd AS RDSellingUsd, " +
            //"RD.SellingLKR AS RDSellingLKR, RD.Weight AS RDWeight, RD.Category AS RDCategory, RD.CategoryId AS RDCategoryId, " +
            //"RD.Material AS RDMaterial, RD.MaterialId AS RDMaterialId, RD.Carratage AS RDCarratage, RD.CarratageId AS RDCarratageId, " +
            //"RD.ShapeId AS RDShapeId, RD.Shape AS RDShape, RD.IsDiamond AS RDIsDiamond, RD.IsActive AS RDIsActive, " +
            //"RD.CompanyId AS RDCompanyId, RD.SaleType AS RDSaleType, RD.CategoryIdSettings AS RDCategoryIdSettings, " +
            //"RD.StockId AS RDStockId, RD.SubCategory AS RDSubCategory, " +
            //"CU.CustId AS CUCustId, CU.Code AS CUCode, CU.Title AS CUTitle, RB.CustomerName AS CUName, CU.LastName AS CULastName, " +
            //"RB.Address AS CUAddress, CU.Address2 AS CUAddress2, RB.City AS CUCity, RB.TpNo AS CUTp1, CU.Tp2 AS CUTp2, " +
            //"RB.Country AS CUCountry, CU.Email AS CUEmail, CU.IsActive AS CUIsActive, CU.CompName AS CUCompName, " +
            //"RB.PassportNo AS CUPassportNo, " +
            //"CA.Name AS CAName, CA.Code AS CACode, CA.Description AS CADescription, " +
            //"IT.ItemID AS ITItemId, IT.ItemCode AS ITItemCode, IT.Name AS ITName, IT.Category AS ITCategory, IT.Cost AS ITCost, " +
            //"IT.Selling AS ITSelling, IT.Wholesale AS ITWholesale, IT.MR AS ITMR, IT.Material AS ITMaterial, " +
            //"IT.MaterialId AS ITMaterialId, IT.Carratage AS ITCarratage, IT.CarratageId AS ITCarratageId, " +
            //"IT.Imgpath AS ITImgpath, IT.SellingLKR AS ITSellingLKR, IT.CostLKR AS ITCostLKR, IT.ShapeID AS ITShapeID, " +
            //"IT.ShapeName AS ITShapeName, IT.UserName AS ITUserName, IT.UserId AS ITUserId, IT.CompName AS ITCompName, " +
            //"IT.IMAGE AS ITImage, IT.IsActive AS ITIsActive, IT.SubCatId AS ITSubCatId, IT.Weight AS ITWeight, " +
            //"SM.Code AS SMCode, SM.Name AS SMName, SM.Address AS SMAddress, SM.Mobile AS SMMobile, SM.Tp AS SMTp, " +
            //"SM.Email AS SMEmail, SM.Nic AS SMNic, IT2.Name AS IT2Name, SH.Name AS SHName," +
            //" CDL.ShapeQty AS CDLShapeQty, CDL.Billid AS RDTranId, " +
            //   " RB.VatAmountLKR AS RBVatAmount, RB.GrossTotalVatLKR AS RBGrossTotalVat, RB.VatRate AS RBVatRate " +
            //"FROM tblItem AS IT2 RIGHT OUTER JOIN " +
            //" tblChangeDesignLog AS CDL ON IT2.ItemID = CDL.DesignId RIGHT OUTER JOIN" +
            //"                  tblShape AS SH ON CDL.ShapeId = SH.Id RIGHT OUTER JOIN" +
            //"                  tblInvoiceDe AS RD INNER JOIN" +
            //"                  tblInvoiceBr AS RB ON RD.BriefId = RB.TranId INNER JOIN" +
            //"                  tblSalesMan AS SM ON RB.SalesManId = SM.Id INNER JOIN" +
            //"               tblCustomer AS CU ON RB.CustId = CU.CustId INNER JOIN" +
            //"               tblCategory AS CA INNER JOIN" +
            //"               tblItem AS IT ON CA.Id = IT.CategoryId INNER JOIN" +
            //"               tblSubCategory AS SUC ON IT.SubCatId = SUC.Id ON RD.ItemId = IT.ItemID ON CDL.BilliD = RB.TranId " +
            //" WHERE (RB.TranId = " + gBillId + ")";

            //            strQuery = "SELECT RB.DateOn AS RBDateOn, RB.RefNo AS RBRefNo, RB.BillNo AS RBBillNo, " +
            //"RB.TranCode AS RBTranCode, RB.GrossTotal AS RBGrossTotal, RB.DisAmount AS RBDisAmount, " +
            //"RB.DisRate AS RBDisRate, RB.GrossTotalVat AS RBNetAmount, RB.PaidAmount AS RBPaidAmount, " +
            //"RB.CNAmount AS RBCNAmount, RB.VatAmount AS RBBalance, RB.IsActive AS RBIsActive, RB.CompanyId AS RBCompanyId, " +
            //"RB.UserId AS RBUserId, RB.ComputerName AS RBComputerName, RB.CreatedOn AS RBCreatedOn, " +
            //"RB.PaymentDes AS RBDescription, RB.UserName AS RBUserName, RB.CompName AS RBCompName, RB.IsGood AS RBIsGood, " +
            //"RB.SalesType AS RBSalesType, RB.SalesManId AS RBSalesManId, RB.GrossTotalLKR AS RBGrossTotalLKR, RB.Status AS RBStatus, " +
            //"RD.BriefId AS RDBriefId, RD.ItemId AS RDItemId, RD.Qty AS RDQty, RD.StkWas AS RDStkWas, " +
            //"RD.LineAmount AS RDLineAmount, RD.PriceUSD AS RDPriceUSD, RD.PriceLKR AS RDPriceLKR, RD.SellingUsd AS RDSellingUsd, " +
            //"RD.SellingLKR AS RDSellingLKR, RD.Weight AS RDWeight, RD.Category AS RDCategory, RD.CategoryId AS RDCategoryId, " +
            //"RD.Material AS RDMaterial, RD.MaterialId AS RDMaterialId, RD.Carratage AS RDCarratage, RD.CarratageId AS RDCarratageId, " +
            //"RD.ShapeId AS RDShapeId, RD.Shape AS RDShape, RD.IsDiamond AS RDIsDiamond, RD.IsActive AS RDIsActive, " +
            //"RD.CompanyId AS RDCompanyId, RD.SaleType AS RDSaleType, RD.CategoryIdSettings AS RDCategoryIdSettings, " +
            //"RD.StockId AS RDStockId, RD.SubCategory AS RDSubCategory, " +
            //"CU.CustId AS CUCustId, CU.Code AS CUCode, CU.Title AS CUTitle, RB.CustomerName AS CUName, CU.LastName AS CULastName, " +
            //"RB.Address AS CUAddress, CU.Address2 AS CUAddress2, RB.City AS CUCity, RB.TpNo AS CUTp1, CU.Tp2 AS CUTp2, " +
            //"RB.Country AS CUCountry, CU.Email AS CUEmail, CU.IsActive AS CUIsActive, CU.CompName AS CUCompName, " +
            //"RB.PassportNo AS CUPassportNo, " +
            //"CA.Name AS CAName, CA.Code AS CACode, CA.Description AS CADescription, " +
            //"IT.ItemID AS ITItemId, IT.ItemCode AS ITItemCode, IT.Name AS ITName, IT.Category AS ITCategory, IT.Cost AS ITCost, " +
            //"IT.Selling AS ITSelling, IT.Wholesale AS ITWholesale, IT.MR AS ITMR, IT.Material AS ITMaterial, " +
            //"IT.MaterialId AS ITMaterialId, IT.Carratage AS ITCarratage, IT.CarratageId AS ITCarratageId, " +
            //"IT.Imgpath AS ITImgpath, IT.SellingLKR AS ITSellingLKR, IT.CostLKR AS ITCostLKR, IT.ShapeID AS ITShapeID, " +
            //"IT.ShapeName AS ITShapeName, IT.UserName AS ITUserName, IT.UserId AS ITUserId, IT.CompName AS ITCompName, " +
            //"IT.IMAGE AS ITImage, IT.IsActive AS ITIsActive, IT.SubCatId AS ITSubCatId, IT.Weight AS ITWeight, " +
            //"SM.Code AS SMCode, SM.Name AS SMName, SM.Address AS SMAddress, SM.Mobile AS SMMobile, SM.Tp AS SMTp, " +
            //"SM.Email AS SMEmail, SM.Nic AS SMNic, IT2.Name AS IT2Name, SH.Name AS SHName," +
            //" CDL.ShapeQty AS CDLShapeQty, CDL.Billid AS RDTranId, " +
            //" RB.VatAmountLKR AS RBVatAmount, RB.GrossTotalVatLKR AS RBGrossTotalVat, RB.VatRate AS RBVatRate " +
            //" FROM tblChangeDesignLog AS CDL INNER JOIN " +
            //    "                      tblShape AS SH ON CDL.ShapeId = SH.Id INNER JOIN " +
            //    "                      tblItem AS IT2 ON CDL.DesignId = IT2.ItemID RIGHT OUTER JOIN" +
            //"                  tblInvoiceDe AS RD INNER JOIN" +
            //"                  tblInvoiceBr AS RB ON RD.BriefId = RB.TranId INNER JOIN" +
            //"                  tblSalesMan AS SM ON RB.SalesManId = SM.Id INNER JOIN" +
            //"               tblCustomer AS CU ON RB.CustId = CU.CustId INNER JOIN" +
            //"               tblCategory AS CA INNER JOIN" +
            //"               tblItem AS IT ON CA.Id = IT.CategoryId INNER JOIN" +
            //"               tblSubCategory AS SUC ON IT.SubCatId = SUC.Id ON RD.ItemId = IT.ItemID ON CDL.BilliD = RB.TranId " +
            //" WHERE (RB.TranId = " + gBillId + ")";

            //strQuery = "SELECT RB.DateOn AS RBDateOn, RB.RefNo AS RBRefNo, RB.BillNo AS RBBillNo, RB.TranCode AS RBTranCode," +
            //    "  RB.GrossTotal AS RBGrossTotal, RB.DisAmount AS RBDisAmount, RB.DisRate AS RBDisRate, " +
            //    " RB.GrossTotalVat AS RBNetAmount, RB.PaidAmount AS RBPaidAmount, RB.CNAmount AS RBCNAmount," +
            //    " RB.VatAmount AS RBBalance, RB.IsActive AS RBIsActive, RB.CompanyId AS RBCompanyId, RB.UserId AS RBUserId, " +
            //    "                         RB.ComputerName AS RBComputerName, RB.CreatedOn AS RBCreatedOn, RB.PaymentDes AS RBDescription, RB.UserName AS RBUserName, RB.CompName AS RBCompName, RB.IsGood AS RBIsGood, " +
            //    "RB.SalesType AS RBSalesType, RB.SalesManId AS RBSalesManId, RB.GrossTotalLKR AS RBGrossTotalLKR, RB.Status AS RBStatus, RD.BriefId AS RDBriefId, RD.ItemId AS RDItemId, RD.Qty AS RDQty, " +
            //    "RD.StkWas AS RDStkWas, RD.LineAmount AS RDLineAmount, RD.PriceUSD AS RDPriceUSD, RD.PriceLKR AS RDPriceLKR, RD.SellingUsd AS RDSellingUsd, RD.SellingLKR AS RDSellingLKR, RD.Weight AS RDWeight, " +
            //    "RD.Category AS RDCategory, RD.CategoryId AS RDCategoryId, RD.Material AS RDMaterial, RD.MaterialId AS RDMaterialId, RD.Carratage AS RDCarratage, RD.CarratageId AS RDCarratageId, RD.ShapeId AS RDShapeId, " +
            //    "RD.Shape AS RDShape, RD.IsDiamond AS RDIsDiamond, RD.IsActive AS RDIsActive, RD.CompanyId AS RDCompanyId, RD.SaleType AS RDSaleType, RD.CategoryIdSettings AS RDCategoryIdSettings, RD.StockId AS RDStockId, " +
            //    "RD.SubCategory AS RDSubCategory, CU.CustId AS CUCustId, CU.Code AS CUCode, CU.Title AS CUTitle, RB.CustomerName AS CUName, CU.LastName AS CULastName, RB.Address AS CUAddress, CU.Address2 AS CUAddress2,  " +
            //    "RB.City AS CUCity, RB.TpNo AS CUTp1, CU.Tp2 AS CUTp2, RB.Country AS CUCountry, CU.Email AS CUEmail, CU.IsActive AS CUIsActive, CU.CompName AS CUCompName, RB.PassportNo AS CUPassportNo, " +
            //    "CA.Name AS CAName, CA.Code AS CACode, CA.Description AS CADescription, IT.ItemID AS ITItemId, IT.ItemCode AS ITItemCode, IT.Name AS ITName, IT.Category AS ITCategory, IT.Cost AS ITCost, IT.Selling AS ITSelling, " +
            //    "IT.Wholesale AS ITWholesale, IT.MR AS ITMR, IT.Material AS ITMaterial, IT.MaterialId AS ITMaterialId, IT.Carratage AS ITCarratage, IT.CarratageId AS ITCarratageId, IT.Imgpath AS ITImgpath, IT.SellingLKR AS ITSellingLKR, " +
            //    "IT.CostLKR AS ITCostLKR, IT.ShapeID AS ITShapeID, IT.ShapeName AS ITShapeName, IT.UserName AS ITUserName, IT.UserId AS ITUserId, IT.CompName AS ITCompName, IT.IMAGE AS ITImage, IT.IsActive AS ITIsActive, " +
            //    "IT.SubCatId AS ITSubCatId, IT.Weight AS ITWeight, SM.Code AS SMCode, SM.Name AS SMName, SM.Address AS SMAddress, SM.Mobile AS SMMobile, SM.Tp AS SMTp, SM.Email AS SMEmail, SM.Nic AS SMNic, " +
            //    "RB.VatAmountLKR AS RBVatAmount, RB.GrossTotalVatLKR AS RBGrossTotalVat, RB.VatRate AS RBVatRate, SH.Name as SHName, CDL.DesignId, IT2.Name AS IT2Name, ISNULL(CDL.ShapeQty, 0) AS CDLShapeQty " +
            //    " FROM            tblChangeDesignLog AS CDL INNER JOIN tblShape AS SH ON CDL.ShapeId = SH.Id INNER JOIN\r\n                         tblItem AS IT2 ON CDL.DesignId = IT2.ItemID RIGHT OUTER JOIN\r\n                         tblInvoiceDe AS RD INNER JOIN\r\n                         tblInvoiceBr AS RB ON RD.BriefId = RB.TranId INNER JOIN\r\n                         tblSalesMan AS SM ON RB.SalesManId = SM.Id INNER JOIN\r\n                         tblCustomer AS CU ON RB.CustId = CU.CustId INNER JOIN\r\n                         tblCategory AS CA INNER JOIN\r\n                         tblItem AS IT ON CA.Id = IT.CategoryId INNER JOIN\r\n                         tblSubCategory AS SUC ON IT.SubCatId = SUC.Id ON RD.ItemId = IT.ItemID ON CDL.DesignId = IT.ItemID " +
            //    "WHERE        (RB.TranId = " + gBillId + ")";

            //strQuery = "SELECT RB.DateOn AS RBDateOn, RB.RefNo AS RBRefNo, RB.BillNo AS RBBillNo, RB.TranCode AS RBTranCode," +
            //    " RB.GrossTotal AS RBGrossTotal, RB.DisAmount AS RBDisAmount, RB.DisRate AS RBDisRate, " +
            //    " RB.GrossTotalVat AS RBNetAmount, RB.PaidAmount AS RBPaidAmount, RB.CNAmount AS RBCNAmount, " +
            //    " RB.VatAmount AS RBBalance, RB.IsActive AS RBIsActive, RB.CompanyId AS RBCompanyId, RB.UserId AS RBUserId, " +
            //    " RB.ComputerName AS RBComputerName, RB.CreatedOn AS RBCreatedOn, RB.PaymentDes AS RBDescription, RB.UserName AS RBUserName," +
            //    " RB.CompName AS RBCompName, RB.IsGood AS RBIsGood, RB.SalesType AS RBSalesType, RB.SalesManId AS RBSalesManId, " +
            //    " RB.GrossTotalLKR AS RBGrossTotalLKR, RB.Status AS RBStatus, RD.BriefId AS RDBriefId, RD.ItemId AS RDItemId, RD.Qty AS RDQty," +
            //    " RD.StkWas AS RDStkWas, RD.LineAmount AS RDLineAmount, RD.PriceUSD AS RDPriceUSD, RD.PriceLKR AS RDPriceLKR," +
            //    "  RD.SellingUsd AS RDSellingUsd, RD.SellingLKR AS RDSellingLKR, RD.Weight AS RDWeight, " +
            //    "RD.Category AS RDCategory, RD.CategoryId AS RDCategoryId, RD.Material AS RDMaterial, RD.MaterialId AS RDMaterialId, " +
            //    " RD.Carratage AS RDCarratage, RD.CarratageId AS RDCarratageId, RD.ShapeId AS RDShapeId, RD.Shape AS RDShape, " +
            //    "RD.IsDiamond AS RDIsDiamond, RD.IsActive AS RDIsActive, RD.CompanyId AS RDCompanyId, RD.SaleType AS RDSaleType, " +
            //    "RD.CategoryIdSettings AS RDCategoryIdSettings, RD.StockId AS RDStockId, RD.SubCategory AS RDSubCategory, " +
            //    "CU.CustId AS CUCustId, CU.Code AS CUCode, CU.Title AS CUTitle, RB.CustomerName AS CUName, CU.LastName AS CULastName, " +
            //    "RB.Address AS CUAddress, CU.Address2 AS CUAddress2, RB.City AS CUCity, RB.TpNo AS CUTp1, CU.Tp2 AS CUTp2, " +
            //    " RB.Country AS CUCountry, CU.Email AS CUEmail, CU.IsActive AS CUIsActive, CU.CompName AS CUCompName," +
            //    "  RB.PassportNo AS CUPassportNo, CA.Name AS CAName, CA.Code AS CACode, CA.Description AS CADescription," +
            //    " IT.ItemID AS ITItemId, IT.ItemCode AS ITItemCode, IT.Name AS ITName, IT.Category AS ITCategory, IT.Cost AS ITCost, " +
            //    "IT.Selling AS ITSelling, IT.Wholesale AS ITWholesale, IT.MR AS ITMR, IT.Material AS ITMaterial," +
            //    " IT.MaterialId AS ITMaterialId, IT.Carratage AS ITCarratage, IT.CarratageId AS ITCarratageId, IT.Imgpath AS ITImgpath, " +
            //    "IT.SellingLKR AS ITSellingLKR, IT.CostLKR AS ITCostLKR, IT.ShapeID AS ITShapeID, IT.ShapeName AS ITShapeName, " +
            //    "IT.UserName AS ITUserName, IT.UserId AS ITUserId, IT.CompName AS ITCompName, IT.IMAGE AS ITImage, IT.IsActive AS ITIsActive," +
            //    " IT.SubCatId AS ITSubCatId, IT.Weight AS ITWeight, SM.Code AS SMCode, SM.Name AS SMName, SM.Address AS SMAddress, " +
            //    "SM.Mobile AS SMMobile, SM.Tp AS SMTp, SM.Email AS SMEmail, SM.Nic AS SMNic, RB.VatAmountLKR AS RBVatAmount, " +
            //    "RB.GrossTotalVatLKR AS RBGrossTotalVat, RB.VatRate AS RBVatRate, SH.Name AS SHName, CDL.DesignId," +
            //    "  ISNULL(CDL.ShapeQty, 0) AS CDLShapeQty, CDL.DesignName AS IT2Name " +
            //    "FROM            tblChangeDesignLog AS CDL INNER JOIN " +
            //  "            tblShape AS SH ON CDL.ShapeId = SH.Id RIGHT OUTER JOIN " +
            //  "            tblInvoiceDe AS RD INNER JOIN" +
            //  "            tblInvoiceBr AS RB ON RD.BriefId = RB.TranId INNER JOIN" +
            //  "            tblSalesMan AS SM ON RB.SalesManId = SM.Id INNER JOIN" +
            //  "            tblCustomer AS CU ON RB.CustId = CU.CustId INNER JOIN" +
            //  "            tblCategory AS CA INNER JOIN" +
            //  "            tblItem AS IT ON CA.Id = IT.CategoryId INNER JOIN" +
            //  "            tblSubCategory AS SUC ON IT.SubCatId = SUC.Id ON RD.ItemId = IT.ItemID ON CDL.BilliD = RB.TranId AND CDL.DesignId = IT.ItemID" +
            //   " WHERE        (RB.TranId = " + gBillId + ")";


            //strQuery = "SELECT RB.DateOn AS RBDateOn, RB.RefNo AS RBRefNo, RB.BillNo AS RBBillNo, RB.TranCode AS RBTranCode," +
            //    " RB.GrossTotal AS RBGrossTotal, RB.DisAmount AS RBDisAmount, RB.DisRate AS RBDisRate, " +
            //    " RB.GrossTotalVat AS RBNetAmount, RB.PaidAmount AS RBPaidAmount, RB.CNAmount AS RBCNAmount, " +
            //    " RB.VatAmount AS RBBalance, RB.IsActive AS RBIsActive, RB.CompanyId AS RBCompanyId, RB.UserId AS RBUserId, " +
            //    " RB.ComputerName AS RBComputerName, RB.CreatedOn AS RBCreatedOn, RB.PaymentDes AS RBDescription, RB.UserName AS RBUserName," +
            //    " RD.Weight AS RBCompName, RB.IsGood AS RBIsGood, RB.SalesType AS RBSalesType, RB.SalesManId AS RBSalesManId, " +
            //    " RB.GrossTotalLKR AS RBGrossTotalLKR, RB.Status AS RBStatus, RD.BriefId AS RDBriefId, RD.ItemId AS RDItemId, RD.Qty AS RDQty," +
            //    " RD.StkWas AS RDStkWas, RD.LineAmount AS RDLineAmount, RD.PriceUSD AS RDPriceUSD, RD.PriceLKR AS RDPriceLKR," +
            //    "  RD.SellingUsd AS RDSellingUsd, RD.SellingLKR AS RDSellingLKR, " +
            //    "RD.Category AS RDCategory, RD.CategoryId AS RDCategoryId, RD.Material AS RDMaterial, RD.MaterialId AS RDMaterialId, " +
            //    " RD.Carratage AS RDCarratage, RD.CarratageId AS RDCarratageId, RD.ShapeId AS RDShapeId, RD.Shape AS RDShape, " +
            //    "RD.IsDiamond AS RDIsDiamond, RD.IsActive AS RDIsActive, RD.CompanyId AS RDCompanyId, RD.SaleType AS RDSaleType, " +
            //    "RD.CategoryIdSettings AS RDCategoryIdSettings, RD.StockId AS RDStockId, RD.SubCategory AS RDSubCategory, " +
            //    "CU.CustId AS CUCustId, CU.Code AS CUCode, CU.Title AS CUTitle, RB.CustomerName AS CUName, CU.LastName AS CULastName, " +
            //    "RB.Address AS CUAddress, CU.Address2 AS CUAddress2, RB.City AS CUCity, RB.TpNo AS CUTp1, CU.Tp2 AS CUTp2, " +
            //    " RB.Country AS CUCountry, CU.Email AS CUEmail, CU.IsActive AS CUIsActive, CU.CompName AS CUCompName," +
            //    "  RB.PassportNo AS CUPassportNo, CA.Name AS CAName, CA.Code AS CACode, CA.Description AS CADescription," +
            //    " IT.ItemID AS ITItemId, IT.ItemCode AS ITItemCode, IT.Name AS ITName, IT.Category AS ITCategory, IT.Cost AS ITCost, " +
            //    "IT.Selling AS ITSelling, IT.Wholesale AS ITWholesale, IT.MR AS ITMR, IT.Material AS ITMaterial," +
            //    " IT.MaterialId AS ITMaterialId, IT.Carratage AS ITCarratage, IT.CarratageId AS ITCarratageId, IT.Imgpath AS ITImgpath, " +
            //    "IT.SellingLKR AS ITSellingLKR, IT.CostLKR AS ITCostLKR, IT.ShapeID AS ITShapeID, IT.ShapeName AS ITShapeName, " +
            //    "IT.UserName AS ITUserName, CDL.TranId AS ITUserId, IT.CompName AS ITCompName, IT.IMAGE AS ITImage, IT.IsActive AS ITIsActive," +
            //    " IT.SubCatId AS ITSubCatId, IT.Weight AS ITWeight, SM.Code AS SMCode, SM.Name AS SMName, SM.Address AS SMAddress, " +
            //    "SM.Mobile AS SMMobile, SM.Tp AS SMTp, SM.Email AS SMEmail, SM.Nic AS SMNic, RB.VatAmountLKR AS RBVatAmount, " +
            //    "RB.GrossTotalVatLKR AS RBGrossTotalVat, RB.VatRate AS RBVatRate, SH.Name AS SHName, CDL.DesignId," +
            //    "  ISNULL(CDL.ShapeQty, 0) AS CDLShapeQty, SC.Name AS IT2Name " +
            //    "FROM            tblChangeDesignLog AS CDL INNER JOIN " +
            //         "      tblShape AS SH ON CDL.ShapeId = SH.Id RIGHT OUTER JOIN " +
            //    "           tblInvoiceDe AS RD INNER JOIN " +
            //       "        tblInvoiceBr AS RB ON RD.BriefId = RB.TranId INNER JOIN " +
            //          "     tblSalesMan AS SM ON RB.SalesManId = SM.Id INNER JOIN " +
            //             "  tblCustomer AS CU ON RB.CustId = CU.CustId INNER JOIN " +
            //              " tblCategory AS CA INNER JOIN " +
            //              " tblItem AS IT ON CA.Id = IT.CategoryId INNER JOIN " +
            //              " tblSubCategory AS SUC ON IT.SubCatId = SUC.Id ON RD.ItemId = IT.ItemID ON CDL.BilliD = RB.TranId LEFT OUTER JOIN " +
            //              " tblSubCategory AS SC ON CDL.SubCategoryid = SC.Id" +
            //   " WHERE        (RB.TranId = " + gBillId + ")";

            //strQuery = "SELECT RB.DateOn AS RBDateOn, RB.RefNo AS RBRefNo, RB.BillNo AS RBBillNo, RB.TranCode AS RBTranCode," +
            //                  " RB.GrossTotal AS RBGrossTotal, RB.DisAmount AS RBDisAmount, RB.DisRate AS RBDisRate, " +
            //                  " RB.GrossTotalVat AS RBNetAmount, RB.PaidAmount AS RBPaidAmount, RB.CNAmount AS RBCNAmount, " +
            //                  " RB.VatAmount AS RBBalance, RB.IsActive AS RBIsActive, RB.CompanyId AS RBCompanyId, RB.UserId AS RBUserId, " +
            //                  " RB.ComputerName AS RBComputerName, RB.CreatedOn AS RBCreatedOn, RB.PaymentDes AS RBDescription, RB.UserName AS RBUserName," +
            //                  " RD.Weight AS RBCompName, RB.IsGood AS RBIsGood, RB.SalesType AS RBSalesType, RB.SalesManId AS RBSalesManId, " +
            //                  " RB.GrossTotalLKR AS RBGrossTotalLKR, RB.Status AS RBStatus, RD.BriefId AS RDBriefId, RD.ItemId AS RDItemId, RD.Qty AS RDQty," +
            //                  " RD.StkWas AS RDStkWas, RD.LineAmount AS RDLineAmount, RD.PriceUSD AS RDPriceUSD, RD.PriceLKR AS RDPriceLKR," +
            //                  "  RD.SellingUsd AS RDSellingUsd, RD.SellingLKR AS RDSellingLKR, " +
            //                  "RD.Category AS RDCategory, RD.CategoryId AS RDCategoryId, RD.Material AS RDMaterial, RD.MaterialId AS RDMaterialId, " +
            //                  " RD.CarratageId AS RDCarratageId, RD.ShapeId AS RDShapeId, RD.Shape AS RDShape, " +
            //                  "RD.IsDiamond AS RDIsDiamond, RD.IsActive AS RDIsActive, RD.CompanyId AS RDCompanyId, RD.SaleType AS RDSaleType, " +
            //                  "RD.CategoryIdSettings AS RDCategoryIdSettings, RD.StockId AS RDStockId, RD.SubCategory AS RDSubCategory, " +
            //                  "CU.CustId AS CUCustId, CU.Code AS CUCode, CU.Title AS CUTitle, RB.CustomerName AS CUName, CU.LastName AS CULastName, " +
            //                  "RB.Address AS CUAddress, CU.Address2 AS CUAddress2, RB.City AS CUCity, RB.TpNo AS CUTp1, CU.Tp2 AS CUTp2, " +
            //                  " RB.Country AS CUCountry, CU.Email AS CUEmail, CU.IsActive AS CUIsActive, CU.CompName AS CUCompName," +
            //                  "  RB.PassportNo AS CUPassportNo, CA.Name AS CAName, CA.Code AS CACode, CA.Description AS CADescription," +
            //                  " IT.ItemID AS ITItemId, IT.ItemCode AS ITItemCode, IT.Name AS ITName, IT.Category AS ITCategory, IT.Cost AS ITCost, " +
            //                  "IT.Selling AS ITSelling, IT.Wholesale AS ITWholesale, IT.MR AS ITMR, IT.Material AS ITMaterial," +
            //                  " IT.MaterialId AS ITMaterialId, RD.Carratage AS ITCarratage, IT.CarratageId AS ITCarratageId, IT.Imgpath AS ITImgpath, " +
            //                  "IT.SellingLKR AS ITSellingLKR, IT.CostLKR AS ITCostLKR, IT.ShapeID AS ITShapeID, IT.ShapeName AS ITShapeName, " +
            //                  "IT.UserName AS ITUserName, CDL.TranId AS ITUserId, IT.CompName AS ITCompName, IT.IMAGE AS ITImage, IT.IsActive AS ITIsActive," +
            //                  " IT.SubCatId AS ITSubCatId, SM.Code AS SMCode, SM.Name AS SMName, CDL.Weight AS SMAddress, " +
            //                  "SM.Mobile AS SMMobile, SM.Tp AS SMTp, SM.Email AS SMEmail, SM.Nic AS SMNic, RB.VatAmountLKR AS RBVatAmount, " +
            //                  "RB.GrossTotalVatLKR AS RBGrossTotalVat, RB.VatRate AS RBVatRate, CDL.DesignId," +
            //                    "  ISNULL(CDL.ShapeQty, 0) AS CDLShapeQty,  CDL.SubCatName AS IT2Name, CDL.ShapeName AS SHName " +
            //                    "FROM tblChangeDesignLog AS CDL RIGHT OUTER JOIN" +
            //                    " tblInvoiceDe AS RD INNER JOIN" +
            //                    " tblInvoiceBr AS RB ON RD.BriefId = RB.TranId INNER JOIN" +
            //                    " tblSalesMan AS SM ON RB.SalesManId = SM.Id INNER JOIN" +
            //                    " tblCustomer AS CU ON RB.CustId = CU.CustId INNER JOIN" +
            //                    " tblCategory AS CA INNER JOIN" +
            //                    " tblItem AS IT ON CA.Id = IT.CategoryId INNER JOIN" +
            //                    "                          tblSubCategory AS SUC ON IT.SubCatId = SUC.Id ON RD.ItemId = IT.ItemID ON CDL.DesignId = IT.ItemID AND CDL.BilliD = RB.TranId" +
            //                    " WHERE (RB.TranId = " + gBillId + ")";

            strQuery = @"
SELECT 
    RB.DateOn AS RBDateOn, 
    RB.RefNo AS RBRefNo, 
    RB.BillNo AS RBBillNo,
    RB.TranCode AS RBTranCode, 
    RB.GrossTotal AS RBGrossTotal, 
    RB.DisAmount AS RBDisAmount, 
    RB.DisRate AS RBDisRate, 
    RB.GrossTotalVat AS RBNetAmount, 
    RB.PaidAmount AS RBPaidAmount, 
    RB.CNAmount AS RBCNAmount, 
    RB.VatAmount AS RBBalance, 
    RB.IsActive AS RBIsActive, 
    RB.CompanyId AS RBCompanyId, 
    RB.UserId AS RBUserId, 
    RB.ComputerName AS RBComputerName, 
    RB.CreatedOn AS RBCreatedOn, 
    RB.PaymentDes AS RBDescription, 
    RB.UserName AS RBUserName,
    RD.Weight AS RBCompName, 
    RB.IsGood AS RBIsGood, 
    RB.SalesType AS RBSalesType, 
    RB.SalesManId AS RBSalesManId, 
    RB.GrossTotalLKR AS RBGrossTotalLKR, 
    RB.Status AS RBStatus, 
    RD.BriefId AS RDBriefId, 
    RD.ItemId AS RDItemId, 
    RD.Qty AS RDQty,
    RD.StkWas AS RDStkWas, 
    RD.LineAmount AS RDLineAmount, 
    RD.PriceUSD AS RDPriceUSD, 
    RD.PriceLKR AS RDPriceLKR,
    RD.SellingUsd AS RDSellingUsd, 
    RD.SellingLKR AS RDSellingLKR, 
    RD.Category AS RDCategory, 
    RD.CategoryId AS RDCategoryId, 
    RD.Material AS RDMaterial, 
    RD.MaterialId AS RDMaterialId, 
    RD.CarratageId AS RDCarratageId, 
    RD.ShapeId AS RDShapeId, 
    RD.Shape AS RDShape, 
    RD.IsDiamond AS RDIsDiamond, 
    RD.IsActive AS RDIsActive, 
    RD.CompanyId AS RDCompanyId, 
    RD.SaleType AS RDSaleType, 
    RD.CategoryIdSettings AS RDCategoryIdSettings, 
    RD.StockId AS RDStockId, 
    RD.SubCategory AS RDSubCategory, 
    CU.CustId AS CUCustId, 
    CU.Code AS CUCode, 
    CU.Title AS CUTitle, 
    RB.CustomerName AS CUName, 
    CU.LastName AS CULastName, 
    RB.Address AS CUAddress, 
    CU.Address2 AS CUAddress2, 
    RB.City AS CUCity, 
    RB.TpNo AS CUTp1, 
    CU.Tp2 AS CUTp2, 
    RB.Country AS CUCountry, 
    CU.Email AS CUEmail, 
    CU.IsActive AS CUIsActive, 
    CU.CompName AS CUCompName,
    RB.PassportNo AS CUPassportNo, 
    CA.Name AS CAName, 
    CA.Code AS CACode, 
    CA.Description AS CADescription,
    IT.ItemID AS ITItemId, 
    IT.ItemCode AS ITItemCode, 
    IT.Name AS ITName, 
    IT.Category AS ITCategory, 
    IT.Cost AS ITCost, 
    IT.Selling AS ITSelling, 
    IT.Wholesale AS ITWholesale, 
    IT.MR AS ITMR, 
    IT.Material AS ITMaterial,
    IT.MaterialId AS ITMaterialId, 
    RD.Carratage AS ITCarratage, 
    IT.CarratageId AS ITCarratageId, 
    IT.Imgpath AS ITImgpath, 
    IT.SellingLKR AS ITSellingLKR, 
    IT.CostLKR AS ITCostLKR, 
    IT.ShapeID AS ITShapeID, 
    IT.ShapeName AS ITShapeName, 
    IT.UserName AS ITUserName, 
    CDL.TranId AS ITUserId, 
    IT.CompName AS ITCompName, 
    IT.IMAGE AS ITImage, 
    IT.IsActive AS ITIsActive,
    IT.SubCatId AS ITSubCatId, 
    SM.Code AS SMCode, 
    SM.Name AS SMName, 
    CDL.Weight AS SMAddress, 
    SM.Mobile AS SMMobile, 
    SM.Tp AS SMTp, 
    SM.Email AS SMEmail, 
    SM.Nic AS SMNic, 
    RB.VatAmountLKR AS RBVatAmount, 
    RB.GrossTotalVatLKR AS RBGrossTotalVat, 
    RB.VatRate AS RBVatRate, 
    CDL.DesignId,
    ISNULL(CDL.ShapeQty, 0) AS CDLShapeQty,  
    CDL.SubCatName AS IT2Name, 
    CDL.ShapeName AS SHName ,
CDL.Tranid as RDTranId
FROM 
    tblInvoiceDe AS RD
    INNER JOIN tblInvoiceBr AS RB 
        ON RD.BriefId = RB.TranId
    INNER JOIN tblSalesMan AS SM 
        ON RB.SalesManId = SM.Id
    INNER JOIN tblCustomer AS CU 
        ON RB.CustId = CU.CustId
    INNER JOIN tblCategory AS CA 
        INNER JOIN tblItem AS IT 
            ON CA.Id = IT.CategoryId
        INNER JOIN tblSubCategory AS SUC 
            ON IT.SubCatId = SUC.Id
        ON RD.ItemId = IT.ItemID
    LEFT JOIN tblChangeDesignLog AS CDL
        ON CDL.DesignId = IT.ItemID 
        AND CDL.BilliD = RB.TranId
        AND CDL.StockId = RD.StockId
WHERE 
    RB.TranId = " + gBillId;



            return strQuery;

            //strQuery = "SELECT RB.DateOn AS RBDateOn, RB.RefNo AS RBRefNo, RB.BillNo AS RBBillNo, RB.TranCode AS RBTranCode," +
            //   " RB.GrossTotal AS RBGrossTotal, RB.DisAmount AS RBDisAmount, RB.DisRate AS RBDisRate, " +
            //   " RB.GrossTotalVat AS RBNetAmount, RB.PaidAmount AS RBPaidAmount, RB.CNAmount AS RBCNAmount, " +
            //   " RB.VatAmount AS RBBalance, RB.IsActive AS RBIsActive, RB.CompanyId AS RBCompanyId, RB.UserId AS RBUserId, " +
            //   " RB.ComputerName AS RDWeight, RB.CreatedOn AS RBCreatedOn, RB.PaymentDes AS RBDescription, RB.UserName AS RBUserName," +
            //   " RB.CompName AS RBCompName, RB.IsGood AS RBIsGood, RB.SalesType AS RBSalesType, RB.SalesManId AS RBSalesManId, " +
            //   " RB.GrossTotalLKR AS RBGrossTotalLKR, RB.Status AS RBStatus, RD.BriefId AS RDBriefId, RD.ItemId AS RDItemId, RD.Qty AS RDQty," +
            //   " RD.StkWas AS RDStkWas, RD.LineAmount AS RDLineAmount, RD.PriceUSD AS RDPriceUSD, RD.PriceLKR AS RDPriceLKR," +
            //   "  RD.SellingUsd AS RDSellingUsd, RD.SellingLKR AS RDSellingLKR," +
            //   "RD.Category AS RDCategory, RD.CategoryId AS RDCategoryId, RD.Material AS RDMaterial, RD.MaterialId AS RDMaterialId, " +
            //   " RD.Carratage AS RDCarratage, RD.CarratageId AS RDCarratageId, RD.ShapeId AS RDShapeId, RD.Shape AS RDShape, " +
            //   "RD.IsDiamond AS RDIsDiamond, RD.IsActive AS RDIsActive, RD.CompanyId AS RDCompanyId, RD.SaleType AS RDSaleType, " +
            //   "RD.CategoryIdSettings AS RDCategoryIdSettings, RD.StockId AS RDStockId, RD.SubCategory AS RDSubCategory, " +
            //   "CU.CustId AS CUCustId, CU.Code AS CUCode, CU.Title AS CUTitle, RB.CustomerName AS CUName, CU.LastName AS CULastName, " +
            //   "RB.Address AS CUAddress, CU.Address2 AS CUAddress2, RB.City AS CUCity, RB.TpNo AS CUTp1, CU.Tp2 AS CUTp2, " +
            //   " RB.Country AS CUCountry, CU.Email AS CUEmail, CU.IsActive AS CUIsActive, CU.CompName AS CUCompName," +
            //   "  RB.PassportNo AS CUPassportNo, CA.Name AS CAName, CA.Code AS CACode, CA.Description AS CADescription," +
            //   " IT.ItemID AS ITItemId, IT.ItemCode AS ITItemCode, IT.Name AS ITName, IT.Category AS ITCategory, IT.Cost AS ITCost, " +
            //   "IT.Selling AS ITSelling, IT.Wholesale AS ITWholesale, IT.MR AS ITMR, IT.Material AS ITMaterial," +
            //   " IT.MaterialId AS ITMaterialId, IT.Carratage AS ITCarratage, IT.CarratageId AS ITCarratageId, IT.Imgpath AS ITImgpath, " +
            //   "IT.SellingLKR AS ITSellingLKR, IT.CostLKR AS ITCostLKR, IT.ShapeID AS ITShapeID, IT.ShapeName AS ITShapeName, " +
            //   "IT.UserName AS ITUserName, IT.UserId AS ITUserId, IT.CompName AS ITCompName, IT.IMAGE AS ITImage, IT.IsActive AS ITIsActive," +
            //   " IT.SubCatId AS ITSubCatId, IT.Weight AS ITWeight, SM.Code AS SMCode, SM.Name AS SMName, SM.Address AS SMAddress, " +
            //   "SM.Mobile AS SMMobile, SM.Tp AS SMTp, SM.Email AS SMEmail, SM.Nic AS SMNic, RB.VatAmountLKR AS RBVatAmount, " +
            //   "RB.GrossTotalVatLKR AS RBGrossTotalVat, RB.VatRate AS RBVatRate, SH.Name AS SHName, CDL.DesignId," +
            //   "  ISNULL(CDL.ShapeQty, 0) AS CDLShapeQty, SC.Name AS IT2Name " +
            //   "FROM            tblChangeDesignLog AS CDL INNER JOIN " +
            // "            tblShape AS SH ON CDL.ShapeId = SH.Id RIGHT OUTER JOIN " +
            // "            tblInvoiceDe AS RD INNER JOIN" +
            // "            tblInvoiceBr AS RB ON RD.BriefId = RB.TranId INNER JOIN" +
            // "            tblSalesMan AS SM ON RB.SalesManId = SM.Id INNER JOIN" +
            // "            tblCustomer AS CU ON RB.CustId = CU.CustId INNER JOIN" +
            // "            tblCategory AS CA INNER JOIN" +
            // "            tblItem AS IT ON CA.Id = IT.CategoryId INNER JOIN" +
            // "                   tblSubCategory AS SUC ON IT.SubCatId = SUC.Id ON RD.ItemId = IT.ItemID ON CDL.BilliD = RB.TranId AND CDL.DesignId = IT.ItemID LEFT OUTER JOIN " +
            // " tblSubCategory AS SC ON CDL.SubCategoryid = SC.Id" +
            //  " WHERE        (RB.TranId = " + gBillId + ")";
            //return strQuery;
        }


        private string GetVatQuery()
        {
            string strqueruy = "";
            strqueruy = "SELECT RB.TranId AS RDBriefID, RD.StockID AS RDStockID, RB.PassportNo AS CUPassportNo, RB.DateOn AS RBDateOn, RB.BillNo AS RBRefNo, RB.BillNo AS RBBillNo, " +
                " RB.Country AS CUCountry, RD.StockId AS RDStockId, RD.QTY AS RDQty, RB.VatAmountLKR AS RBVatAmount, RB.GrossTotalVatLKR AS RBGrossTotalVat, RD.SellingLKR AS RDSellingLKR," +
                "  CASE    WHEN SubItems.SubItemList IS NOT NULL THEN IT.Name + ' ' + SubItems.SubItemList ELSE IT.Name END AS ITName " +
                " FROM tblInvoiceDe AS RD " +
                " INNER JOIN tblInvoiceBr AS RB ON RD.BriefId = RB.TranId " +
                "INNER JOIN tblSalesMan AS SM ON RB.SalesManId = SM.Id " +
                " INNER JOIN tblCustomer AS CU ON RB.CustId = CU.CustId" +
                " INNER JOIN tblCategory AS CA " +
                " INNER JOIN tblItem AS IT ON CA.Id = IT.CategoryId " +
                " INNER JOIN tblSubCategory AS SUC ON IT.SubCatId = SUC.Id" +
                "   ON RD.ItemId = IT.ItemID LEFT JOIN ( " +
                "SELECT BilliD, DesignId, STRING_AGG(SubCatName, ', ') AS SubItemList FROM tblChangeDesignLog GROUP BY BilliD, DesignId) AS SubItems " +
                " ON SubItems.DesignId = IT.ItemID " +
                " AND SubItems.BilliD = RB.TranId " +
                "WHERE RB.TranId = " + gBillId + "";
            return strqueruy;
        }
        private void btnShow_Click(object sender, EventArgs e)
        {
            //LoadReport();

            InsertSampleReportData();

            ReportPrinter.LoadInvoiceReport(GetRQuery(), CommonVariables.gReportLocation + "\\Medi.rpt", "", "", cryRpt);
        }

        private void txtRefNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadReport();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        { 
            ReportPrinter.PrintReport(GetVatQuery(), "INVOICE", "", "", CommonVariables.gReportLocation + "\\VAT.rpt");

        }

        private void cboCashorCard_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public static void InsertMainHeader(
    string mainHeader,
    string cap1,
    string cap2,
    string cap3,
    string cap4,
    int mainOrder)
        { 

            string q = @"
INSERT INTO rptFlatReport
(MainHeader, Caption1, Caption2, Caption3, Caption4,
 RowType, MainOrder)
VALUES
(@MainHeader,@C1,@C2,@C3,@C4,1,@MainOrder)";

            SqlCommand cmd = new SqlCommand(q, GlobalData.GConnection);

            cmd.Parameters.AddWithValue("@MainHeader", mainHeader);
            cmd.Parameters.AddWithValue("@C1", (object)cap1 ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@C2", (object)cap2 ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@C3", (object)cap3 ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@C4", (object)cap4 ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MainOrder", mainOrder);
             
            cmd.ExecuteNonQuery();
        }
        public static void InsertSubHeader(
    string mainHeader,
    string subHeader,
    int mainOrder,
    int subOrder)
        { 
            string q = @"
INSERT INTO rptFlatReport
(MainHeader, SubHeader, RowType, MainOrder, SubOrder)
VALUES
(@Main,@Sub,2,@MainOrder,@SubOrder)";

            SqlCommand cmd = new SqlCommand(q, GlobalData.GConnection);

            cmd.Parameters.AddWithValue("@Main", mainHeader);
            cmd.Parameters.AddWithValue("@Sub", subHeader);
            cmd.Parameters.AddWithValue("@MainOrder", mainOrder);
            cmd.Parameters.AddWithValue("@SubOrder", subOrder);
             
            cmd.ExecuteNonQuery();
        }
        public static void InsertDetail(
    string mainHeader,
    string subHeader,
    string detailName,
    decimal? v1,
    decimal? v2,
    decimal? v3,
    decimal? v4,
    int mainOrder,
    int subOrder,
    int detailOrder)
        { 
            string q = @"
INSERT INTO rptFlatReport
(MainHeader,SubHeader,DetailName,
 Val1,Val2,Val3,Val4,
 RowType,MainOrder,SubOrder,DetailOrder)
VALUES
(@Main,@Sub,@Detail,
 @V1,@V2,@V3,@V4,
 3,@MainOrder,@SubOrder,@DetailOrder)";

            SqlCommand cmd = new SqlCommand(q, GlobalData.GConnection);

            cmd.Parameters.AddWithValue("@Main", mainHeader);
            cmd.Parameters.AddWithValue("@Sub", subHeader);
            cmd.Parameters.AddWithValue("@Detail", detailName);

            cmd.Parameters.AddWithValue("@V1", v1 ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@V2", v2 ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@V3", v3 ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@V4", v4 ?? (object)DBNull.Value);

            cmd.Parameters.AddWithValue("@MainOrder", mainOrder);
            cmd.Parameters.AddWithValue("@SubOrder", subOrder);
            cmd.Parameters.AddWithValue("@DetailOrder", detailOrder);
             
            cmd.ExecuteNonQuery();
        }
        public static void ClearReport()
        { 
            SqlCommand cmd = new SqlCommand(
                "DELETE FROM rptFlatReport", GlobalData.GConnection);
             
            cmd.ExecuteNonQuery();
        }
        // =========================
        // BUILD YOUR SAMPLE DATA
        // =========================
        public static void InsertSampleReportData()
        {
            ClearReport();

            // ===== MAIN HEADER 1 =====
            InsertMainHeader(
                "1. MAIN HEADER",
                "Credit",
                "Debit",
                "Discount",
                null,
                1);

            InsertSubHeader("1. MAIN HEADER", "Sub Header", 1, 1);

            InsertDetail(
                "1. MAIN HEADER",
                "Sub Header",
                "1.Detail1",
                1000, null, 5, null,
                1, 1, 1);

            InsertSubHeader("1. MAIN HEADER", "Sub Header 2", 1, 2);

            InsertDetail(
                "1. MAIN HEADER",
                "Sub Header 2",
                "AAAA",
                500, null, null, null,
                1, 2, 1);

            InsertSubHeader("1. MAIN HEADER", "Sub Header 3", 1, 3);


            // ===== MAIN HEADER 2 =====
            InsertMainHeader(
                "2. MAIN HEADER",
                "CASH CR",
                "CASH DR",
                "CARD",
                "CHQ",
                2);

            InsertSubHeader("2. MAIN HEADER", "Sub Header", 2, 1);

            InsertDetail(
                "2. MAIN HEADER",
                "Sub Header",
                "1.Detail1",
                1000, null, null, null,
                2, 1, 1);

            InsertSubHeader("2. MAIN HEADER", "Sub Header 2", 2, 2);

            InsertDetail(
                "2. MAIN HEADER",
                "Sub Header 2",
                "AAAA",
                null, 1000, null, null,
                2, 2, 1);

            InsertSubHeader("2. MAIN HEADER", "Sub Header 3", 2, 3);

            InsertDetail(
                "2. MAIN HEADER",
                "Sub Header 3",
                "CARD SALE",
                null, null, 500, null,
                2, 3, 1);
        }

        private string GetRQuery()
        {
            string q = @"
SELECT RowID,MainHeader as RBDescription,SubHeader as RBIsActive,DetailName as RBUserName,
       Caption1 as RBRefNo,Caption2 as RBComputerName,Caption3 as RDCategory,Caption4 as RDMaterial,
       Val1 as RDPriceUSD,Val2 as RDPriceLKR,Val3 as RDSellingUsd,Val4 as RDSellingLKR,
       RowType as RDItemId,MainOrder as RBUserId,SubOrder as RBTranId,DetailOrder as RDMaterialId
FROM rptFlatReport
ORDER BY MainOrder,SubOrder,DetailOrder";
            return q;
        }
    }
}
