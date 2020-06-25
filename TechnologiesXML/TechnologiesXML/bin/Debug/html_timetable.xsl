<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:output method="html" encoding="utf-8"/>
  <xsl:template match="/">
    <HTML>
      <BODY>
        <style>
          body {
            background-image: url('back.jpg');
          }
          table {
            font-family: "Lucida Sans Unicode", "Times New Roman", Sans-Serif;
            border-collapse: collapse;
            text-align: center;
            position: relative;
            margin: auto;
            border: 1px solid;
          }
          th {
            font-size: 16px;
            font-weight: 500;
            background: lightgreen;
            color: black;
            padding: 15px 25px;
            border-style: solid;
            border-width: 0 1px 1px 0;
            border-color: black;
          }
          .th_bold {
            font-size: 17px;
            font-weight: bold;
            color: #ff5050;
          }
          span {
            font-style: italic;
            font-size: 13px;
          }
        </style>
        <table>
          <xsl:for-each select="//day">
            <tr>
              <th></th>
              <th class="th_bold">
                <xsl:if test="@num = 1">
                  Понедельник
                </xsl:if>
                <xsl:if test="@num = 2">
                  Вторник
                </xsl:if>
                <xsl:if test="@num = 3">
                  Среда
                </xsl:if>
                <xsl:if test="@num = 4">
                  Четверг
                </xsl:if>
                <xsl:if test="@num = 5">
                  Пятница
                </xsl:if>
              </th>
            </tr>
            <xsl:for-each select="lesson">
              <tr>
                <th>
                  <xsl:value-of select="concat(substring-before(./time_begin,':'), ':', substring-before(substring-after(./time_begin, ':'), ':'))"/> - <xsl:value-of select="concat(substring-before(./time_end,':'), ':', substring-before(substring-after(./time_end, ':'), ':'))"/>
                </th>
                <th>
                  <xsl:value-of select="@title"/>
                  <br/>
                  <span>
                  <xsl:value-of select="teacher"/> (<xsl:value-of select="audience"/>)
                   </span>
                </th>
              </tr>
            </xsl:for-each>
          </xsl:for-each>
        </table>
      </BODY>
    </HTML>
  </xsl:template>
</xsl:stylesheet>
